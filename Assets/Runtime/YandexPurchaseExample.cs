using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YandexPurchaseExample : MonoBehaviour
{
    public PurchaseUI purchaseUi;
    private YandexInApp _yandexInApp = new YandexInApp();
    private static string TestInappID = "test_purchase";
    private static int testInappReward = 10;
    private Dictionary<string, Action> _purchaseActions = new Dictionary<string, Action>();
    private int gold = 0;
    
    private void Awake()
    {
        _yandexInApp.Init();
        _purchaseActions.Add(TestInappID, () =>
        {
            gold += testInappReward;
            UpdateGoldLabe();
        });
        UpdateGoldLabe();
    }

    private void UpdateGoldLabe()
    {
        purchaseUi.Gold = gold;
    }
    
    private void OnEnable()
    {
        _yandexInApp.OnPurchaseSuccess += OnOnPurchaseSuccess;
        _yandexInApp.OnPurchasesInitialized += OnPurchasesInitialized;
    }
    
    private void OnDisable()
    {
        _yandexInApp.OnPurchaseSuccess -= OnOnPurchaseSuccess;
        _yandexInApp.OnPurchasesInitialized -= OnPurchasesInitialized;
    }

    private void OnPurchasesInitialized(List<Product> products)
    {
        foreach (var product in products)
        {
            var panel = Instantiate(purchaseUi.Panel, purchaseUi.Content);
            panel.Title = product.title;
            panel.Description = product.description;
            panel.ButtonText = product.priceValue + " " + product.priceCurrencyCode;
            StartCoroutine(LoadIMG(product.imageURI, panel));
            panel.ButtonListener = () =>
            {
                _yandexInApp.Purchase(product.id);
            };
        }
    }

    private IEnumerator LoadIMG(string url, PurchasePanel panel)
    {
            var www = new WWW(url);
            yield return www;
            panel.Icon = www.texture;
    }
    
  
    
    private void OnOnPurchaseSuccess(string productId)
    {
        if (_purchaseActions.ContainsKey(productId))
        {
            _purchaseActions[productId]?.Invoke();
        }
    }

   
    
    
}
