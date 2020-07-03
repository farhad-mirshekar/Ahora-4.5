using DNTScheduler;
using FM.Portal.Core.LucenceSearch.Product;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.DataSource;
using FM.Portal.Domain;
using FM.Portal.Infrastructure.DAL;
using System;
using System.Threading.Tasks;
using Unity;

namespace Ahora.WebApp.WebTasks
{
    public class ReIndexLuceneTask : ScheduledTaskTemplate
    {
        /// <summary>
        /// اگر چند جاب در يك زمان مشخص داشتيد، اين خاصيت ترتيب اجراي آن‌ها را مشخص خواهد كرد
        /// </summary>
        public override int Order => 1;

        public override bool RunAt(DateTime utcNow)
        {
            if (this.IsShuttingDown || this.Pause)
                return false;

            var now = utcNow.AddHours(3.5);

            return now.Hour == 2 &&
                   now.Minute == 1 && now.Second == 1;
        }

        public override async Task RunAsync()
        {
            if (this.IsShuttingDown || this.Pause)
                return;

            LucenceProductIndexSearch.ClearLuceneIndex();

            var container = new UnityContainer();
            container.RegisterType<IProductDataSource, ProductDataSource>();
            container.RegisterType<IProductService, ProductService>();
            var _productService = container.Resolve<IProductService>();

            foreach (var product in _productService.List().Data)
            {
                LucenceProductIndexSearch.ClearLuceneIndexRecord(product.ID);
                LucenceProductIndexSearch.AddUpdateLuceneIndex(new Product
                {
                   Name = product.Name
                });
            }
        }

        public override string Name => "ReIndexLuceneTask";
    }
}