using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

using Colyseus.Schema;
public class RootManager : VisualElement
{
    #region SCreens
    VisualElement InGame;
    VisualElement Shop;
    #endregion

    Button OpenShopButton;
    Button CloseShopButton;

    public new class UxmlFactory : UxmlFactory<RootManager, UxmlTraits> { };
    public new class UxmlTraits : VisualElement.UxmlTraits { };

    public RootManager()
    {

        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);

    }

    private void OnGeometryChange(GeometryChangedEvent evt)
    {
        InGame = this.Q<VisualElement>("InGame");
        Shop = this.Q<VisualElement>("Shop");

        OpenShopButton = InGame.Q<Button>("shop-button");
        OpenShopButton.RegisterCallback<ClickEvent>((ClickEvent ev) => EnableShop());

        CloseShopButton = Shop.Q<Button>("close-shop");
        CloseShopButton.RegisterCallback<ClickEvent>((ClickEvent ev) => EnableInGame());

        this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    private void EnableShop()
    {
        DisableAllScreens();
        Shop.style.display = DisplayStyle.Flex;
    }
    private void EnableInGame()
    {
        DisableAllScreens();
        InGame.style.display = DisplayStyle.Flex;
    }

    private void DisableAllScreens()
    {
        Shop.style.display = DisplayStyle.None;
        InGame.style.display = DisplayStyle.None;
    }
}