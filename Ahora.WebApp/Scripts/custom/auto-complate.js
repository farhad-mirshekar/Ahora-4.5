$(function () {
            $.widget("custom.catcomplete", $.ui.autocomplete, {
                _create: function () {
                    this._super();
                    this.widget().menu("option", "items", "> :not(.ui-autocomplete-category)");
                },
                _renderMenu: function (ul, items) {
                    var that = this,
                        currentCategory = "";
                    $.each(items, function (index, item) {
                        var li;
                        if (item.category != currentCategory) {
                            ul.append("<li class='ui-autocomplete-category'>" + item.category + "</li>");
                            currentCategory = item.category;
                        }
                        li = that._renderItemData(ul, item);
                        if (item.category) {
                            li.attr("aria-label", item.category + " : " + item.label);
                        }
                    });
                }
            });
            $("#searchInput").catcomplete({
                delay: 300,
                source: function (request, response) {
                    $.ajax({
                        url: '/search/AutoCompleteSearch?term=' + request.term,
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data, function (item) {
                                return { label: item.Label, value: item.Label, url: item.Url, category:item.Category };
                            }))
                        },
                        //error: function (response) {
                        //    alert(response.responseText);
                        //},
                        //failure: function (response) {
                        //    alert(response.responseText);
                        //}
                    });
                },
                minLength: 2,
                dir: 'rtl', minChars: 2,
                mustMatch: false, max: 20, autoFill: false,
                matchContains: false, scroll: false, width: 300,
                select: function (event, ui) {
                    window.location = ui.item.url;
                },
                position: { my: "right top", at: "right bottom", collision: "none" },
            })

        });