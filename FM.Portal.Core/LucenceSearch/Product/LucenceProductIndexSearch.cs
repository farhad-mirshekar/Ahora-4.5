using FM.Portal.Core.Common;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Version = Lucene.Net.Util.Version;
namespace FM.Portal.Core.LucenceSearch.Product
{
   public class LucenceProductIndexSearch
    {
        private const Version _version = Version.LUCENE_30;

        private static readonly string _luceneDir = HttpRuntime.AppDomainAppPath + @"App_Data\Lucene_Index";

        public static void MapProductToLucence(Core.Model.Product model, IndexWriter writer)
        {
            var document = new Document();
            document.Add(new Field("ID", model.ID.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("Title", model.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));

            // add entry to index
            writer.AddDocument(document);
        }
        public static bool ClearLuceneIndex()
        {
            try
            {
                var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
                using (var writer = new IndexWriter(_directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    // remove older index entries
                    writer.DeleteAll();

                    // close handles
                    analyzer.Close();
                    writer.Dispose();
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }
        public static void ClearLuceneIndexRecord(Guid ProductID)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(_version);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                var searchQuery = new TermQuery(new Term(StronglyTyped.PropertyName<Model.Product>(x => x.ID), ProductID.ToString()));


                // remove older index entry
                writer.DeleteDocuments(searchQuery);

                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }
        public static void AddUpdateLuceneIndex(Model.Product modelData)
        {
            AddUpdateLuceneIndex(new List<Model.Product> { modelData });
        }
        public static void AddUpdateLuceneIndex(IEnumerable<Model.Product> modelData)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(_version);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older entry if any)
                foreach (var data in modelData)
                    MapProductToLucence(data, writer);

                // close handles
                analyzer.Close();
                writer.Optimize();
                writer.Commit();
                writer.Dispose();
            }
        }
        public static IEnumerable<Model.Product> Search(string input, params string[] fieldsName)
        {
            if (string.IsNullOrEmpty(input))
                return new List<Model.Product>();

            IEnumerable<string> terms = input.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*");
            input = string.Join(" ", terms);
            return _search(input, fieldsName);
        }


        private static Query parseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }
        private static IEnumerable<Core.Model.Product> _search(string searchQuery, string[] searchFields)
        {
            // validation
            if (string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", "")))
                return new List<Core.Model.Product>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(_directory, false))
            {
                const int hitsLimit = 1000;
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);


                var parser = new MultiFieldQueryParser
                    (Version.LUCENE_30, searchFields, analyzer);
                Query query = parseQuery(searchQuery, parser);
                ScoreDoc[] hits = searcher.Search(query, null, hitsLimit, Sort.RELEVANCE).ScoreDocs;

                if (hits.Length == 0)
                {
                    searchQuery = searchByPartialWords(searchQuery);
                    query = parseQuery(searchQuery, parser);
                    hits = searcher.Search(query, hitsLimit).ScoreDocs;
                }

                var results = _mapLuceneToDataList(hits, searcher);
                analyzer.Close();
                searcher.Dispose();
                return results;
            }
        }
        private static string searchByPartialWords(string bodyTerm)
        {
            bodyTerm = bodyTerm.Replace("*", "").Replace("?", "");
            IEnumerable<string> terms = bodyTerm.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Trim() + "*");
            bodyTerm = string.Join(" ", terms);
            return bodyTerm;
        }
        private static FSDirectory _directory
        {
            get
            {
                //if (_directoryTemp == null)
                var directoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
                //if (IndexWriter.IsLocked(_directoryTemp))
                //    IndexWriter.Unlock(_directoryTemp);
                //string lockFilePath = Path.Combine(_luceneDir, "write.lock");
                //if (File.Exists(lockFilePath))
                //    File.Delete(lockFilePath);
                return directoryTemp;
            }
        }
        private static IEnumerable<Core.Model.Product> _mapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(_mapLuceneDocumentToData).ToList();
        }

        private static IEnumerable<Core.Model.Product> _mapLuceneToDataList(IEnumerable<ScoreDoc> hits,
            IndexSearcher searcher)
        {
            return hits.Select(hit => _mapLuceneDocumentToData(searcher.Doc(hit.Doc))).ToList();
        }
        private static Model.Product _mapLuceneDocumentToData(Document doc)
        {
            return new Core.Model.Product
            {
                ID = SQLHelper.CheckGuidNull(doc.Get(StronglyTyped.PropertyName<Model.Product>(x => x.ID))),
                Name = SQLHelper.CheckStringNull(doc.Get(StronglyTyped.PropertyName<Model.Product>(x => x.Name))),
            };
        }
    }
}
