using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Input;
using Library.DB;
using Library.DTO;
using WarehouseInventory.Windows;

namespace WarehouseInventory.ViewModels;

public class MainViewModel : BaseViewModel
{
        private readonly HttpClient _httpClient;
        private ObservableCollection<ProductDTO> _products = new();
        private string _searchQuery = "";
        private ProductDTO? _selectedProduct;

        public ObservableCollection<ProductDTO> Products
        {
            get => _products;
            set { _products = value; OnPropertyChanged(); }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set 
            { 
                _searchQuery = value; 
                OnPropertyChanged();
                _ = SearchAsync();
            }
        }

        public ProductDTO? SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public ICommand EditProductCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand NavigateToChartCommand { get; }
        public ICommand NavigateToMovementCommand { get; }
        public ICommand NavigateToInvoiceCommand { get; }
        public ICommand NavigateToCustomerCommand { get; }
        public ICommand NavigateToSupplierCommand { get; }

        public event Action? OnLogout;

        public MainViewModel()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5059/api/");
            
            
            
            EditProductCommand = new RelayCommand(_ => OpenProductEditWindow(SelectedProduct), _ => SelectedProduct != null);
            LogoutCommand = new RelayCommand(_ => OnLogout?.Invoke());
            
            _ = LoadProductsAsync();
        }

        private async System.Threading.Tasks.Task LoadProductsAsync()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<ProductDTO[]>("Products");
                if (products != null)
                {
                    Products.Clear();
                    foreach (var product in products)
                        Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка загрузки: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async System.Threading.Tasks.Task SearchAsync()
        {
            try
            {
                var allProducts = await _httpClient.GetFromJsonAsync<ProductDTO[]>("Products");
                if (allProducts != null)
                {
                    var filtered = string.IsNullOrWhiteSpace(SearchQuery)
                        ? allProducts
                        : allProducts.Where(p => p.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToArray();
                    
                    Products.Clear();
                    foreach (var product in filtered)
                        Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Ошибка поиска: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenProductEditWindow(ProductDTO? product)
        {
            if (product == null)
            {
                MessageBox.Show("❌ Выберите товар для редактирования", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            var window = new ProductsEditWindow(product);
            window.Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            window.ShowDialog();
            _ = LoadProductsAsync();
        }
        
        
}