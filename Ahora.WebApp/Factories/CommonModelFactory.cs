﻿using Ahora.WebApp.Models.App;
using Ahora.WebApp.Models.Ptl;
using FM.Portal.Core.Caching;
using FM.Portal.Core.Common;
using FM.Portal.Core.Model;
using FM.Portal.Core.Service;
using FM.Portal.FrameWork.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ahora.WebApp.Factories
{
    public class CommonModelFactory : ICommonModelFactory
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IDiscountService _discountService;
        private readonly IAttachmentService _attachmentService;
        private readonly ICacheManager _cacheManager;
        private readonly ICategoryMapDiscountService _categoryMapDiscountService;
        private readonly ISliderService _sliderService;
        public CommonModelFactory(IProductService productService
                                  , ICategoryService categoryService
                                  , IDiscountService discountService
                                  , IAttachmentService attachmentService
                                  , ICacheManager cacheManager
                                  , ICategoryMapDiscountService categoryMapDiscountService
                                  , ISliderService sliderService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _discountService = discountService;
            _attachmentService = attachmentService;
            _cacheManager = cacheManager;
            _categoryMapDiscountService = categoryMapDiscountService;
            _sliderService = sliderService;
        }
        public List<ProductOverviewModel> TrendingProduct(Guid? LanguageID)
        {
            try
            {
                return _cacheManager.Get("Ahora.TrendigProduct", () => 
                {
                    var productsResult = _productService.List(new ProductListVM() { SpecialOffer = true, PageSize = Helper.CountShowProduct });
                    if (!productsResult.Success)
                        return null;
                    var products = productsResult.Data;
                    var productOverviewModel = productsResult.Data.Select(p => new ProductOverviewModel()
                    {
                        CallForPrice = p.CallForPrice,
                        Discount = p.Discount,
                        DiscountType = p.DiscountType,
                        ID = p.ID,
                        Name = p.Name,
                        Price = p.Price,
                        CategoryID = p.CategoryID,
                        hasDiscount = p.HasDiscount
                    }).ToList();

                    foreach (var item in productOverviewModel)
                    {
                        var categoryResult = _categoryService.Get(item.CategoryID);
                        if (!categoryResult.Success)
                            item.Category = null;
                        else
                            item.Category = categoryResult.Data;
                    }

                    foreach (var item in productOverviewModel)
                    {
                        if (item.Category != null)
                        {
                            if (item.Category.HasDiscountsApplied)
                            {
                                var discountCategoryResult = _categoryMapDiscountService.Get(item.CategoryID, null);
                                if (!discountCategoryResult.Success)
                                    item.CategoryDiscount = null;
                                else
                                {
                                    var discountResult = _discountService.Get(discountCategoryResult.Data.DiscountID);
                                    if (!discountResult.Success)
                                        item.CategoryDiscount = null;
                                    else
                                        item.CategoryDiscount = discountResult.Data;
                                }
                            }
                        }
                    }
                    foreach (var item in productOverviewModel)
                    {
                        var attachmentsResult = _attachmentService.List(item.ID);
                        if (!attachmentsResult.Success)
                            item.PictureAttachment = null;
                        item.PictureAttachment = attachmentsResult.Data.Where(a => a.Type == AttachmentType.اصلی).FirstOrDefault();
                    }

                    return productOverviewModel;
                });
            }
            catch (Exception e) { return null; }
        }

        public List<ProductOverviewModel> HasDiscountProduct(Guid? LanguageID)
        {
            try
            {
                return _cacheManager.Get("Ahora.HasDiscountProduct", () =>
                {
                    var productsResult = _productService.List(new ProductListVM() { HasDiscount = HasDiscountType.دارای_تخفیف, PageSize = Helper.CountShowProduct });
                    if (!productsResult.Success)
                        return null;
                    var products = productsResult.Data;
                    var productOverviewModel = productsResult.Data.Select(p => new ProductOverviewModel()
                    {
                        CallForPrice = p.CallForPrice,
                        Discount = p.Discount,
                        DiscountType = p.DiscountType,
                        ID = p.ID,
                        Name = p.Name,
                        Price = p.Price,
                        CategoryID = p.CategoryID,
                        hasDiscount = p.HasDiscount
                    }).ToList();

                    foreach (var item in productOverviewModel)
                    {
                        var categoryResult = _categoryService.Get(item.CategoryID);
                        if (!categoryResult.Success)
                            item.Category = null;
                        else
                            item.Category = categoryResult.Data;
                    }

                    foreach (var item in productOverviewModel)
                    {
                        if (item.Category != null)
                        {
                            if (item.Category.HasDiscountsApplied)
                            {
                                var discountCategoryResult = _categoryMapDiscountService.Get(item.CategoryID, null);
                                if (!discountCategoryResult.Success)
                                    item.CategoryDiscount = null;
                                else
                                {
                                    var discountResult = _discountService.Get(discountCategoryResult.Data.DiscountID);
                                    if (!discountResult.Success)
                                        item.CategoryDiscount = null;
                                    else
                                        item.CategoryDiscount = discountResult.Data;
                                }
                            }
                        }
                    }
                    foreach (var item in productOverviewModel)
                    {
                        var attachmentsResult = _attachmentService.List(item.ID);
                        if (!attachmentsResult.Success)
                            item.PictureAttachment = null;
                        item.PictureAttachment = attachmentsResult.Data.Where(a => a.Type == AttachmentType.اصلی).FirstOrDefault();
                    }

                    return productOverviewModel;
                });
            }
            catch (Exception e) { return null; }
        }

        public SliderListModel Sliders(int Count)
        {
            try
            {
                var slidersResult = _sliderService.List(new SliderListVM() { PageSize = Count, Enabled = EnableMenuType.فعال });
                if (!slidersResult.Success)
                    return null;

                var sliders = slidersResult.Data;

                if(sliders != null && sliders.Count > 0)
                {
                    var sliderListModel = new SliderListModel();
                    sliders.ForEach(slider =>
                    {
                        sliderListModel.AvailableSliders.Add(slider.ToModel());
                    });

                    sliderListModel.AvailableSliders.ForEach(slider =>
                    {
                        var attachmentResult = _attachmentService.List(slider.ID);
                        if (attachmentResult.Success)
                        {
                            slider.PictureAttachments = attachmentResult.Data;
                        }
                    });

                    return sliderListModel;
                }

                return null;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}