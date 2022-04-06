using System;
using System.Collections.Generic;
using UnityEngine;

public class YandexInApp : IYandexPurchasesListener
{
    private List<Product> _inapps = new List<Product>();
    public event Action<string> OnPurchaseSuccess;
    public event Action<string> OnPurchaseFailed;
    public event Action<Products> OnPurchasesReceived;
    public event Action<List<Product>> OnPurchasesInitialized;
    
    public YandexInApp()
    {
        YandexPurchasesHandler.SetListener(this);
    }
    
    public void Init()
    { 
        Debug.Log("YandexInApp Init");
        YandexGames.InitPurchase();
    }
    
    public void Purchase(string productId)
    {
        YandexPurchasesHandler.Purchase(productId);
    }
    
    public void GetPurchases()
    {
        YandexPurchasesHandler.GetPurchases();
    }


    public string GetPriceAndCurrency(string productId)
    {
        return GetPrice(productId) + " " + GetCurrency(productId);
    }
    
    public string GetPrice(string productId)
    {
        return _inapps.Find(it => it.id.Contains(productId)).priceValue;
    }
    
    public string GetCurrency(string productId)
    {
        return _inapps.Find(it => it.id.Contains(productId)).priceCurrencyCode;
    }

    public string GetProduct(string productId)
    {
        return _inapps.Find(it => it.id.Contains(productId)).priceCurrencyCode;
    }

    public void YandexProductsReceived(Products products)
    {
        Dispatcher.RunOnMainThread(() =>
        {
            foreach (var product in products.products)
            {
                _inapps.Add(product);
            }

            foreach (var inapp in _inapps)
            {
                Debug.Log("Yandex product " + inapp.id + " is available");
            }
            OnPurchasesInitialized?.Invoke(_inapps);
        });
        
    }
    
    public void PurchasesReceived (Products products)
    {
        Dispatcher.RunOnMainThread(() => { OnPurchasesReceived?.Invoke(products); });
    }

    public void PurchaseSuccess(string productId)
    {
        Dispatcher.RunOnMainThread(() => {OnPurchaseSuccess?.Invoke(productId);});
    }

    public void PurchaseFailed(string productId)
    {
        Dispatcher.RunOnMainThread(() => {OnPurchaseFailed?.Invoke(productId);});
    }
}