using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        List<Produto> tmp = await App.Db.GetAll();

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;

        lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            double soma = lista.Sum(i => i.Total);
            string msg = $"O total é {soma:C}";
            DisplayAlert("Total dos Produtos", msg, "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"Ocorreu um erro ao calcular o total: {ex.Message}", "OK");
        }
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            var menuItem = sender as MenuItem;
            var produto = menuItem?.BindingContext as Produto;

            if (produto != null)
            {
                bool result = await DisplayAlert("Remover", $"Deseja remover {produto.Descricao}?", "Sim", "Não");

                if (result)
                {
                    lista.Remove(produto);
                    await App.Db.delete(produto);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro ao remover o item: {ex.Message}", "OK");
        }
    }
}