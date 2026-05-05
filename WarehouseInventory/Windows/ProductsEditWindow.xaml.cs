using System.Windows;
using Library.DB;
using Library.DTO;
using WarehouseInventory.ViewModels;

namespace WarehouseInventory.Windows;

public partial class ProductsEditWindow : Window
{
    public ProductsEditWindow(ProductDTO product)
    {
        InitializeComponent();
        
        var vm = new ProductEditViewModel(product);
        vm.SetClose(Close);
        DataContext = vm;
    }
}