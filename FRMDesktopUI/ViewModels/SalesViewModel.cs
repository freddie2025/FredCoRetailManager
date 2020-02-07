using Caliburn.Micro;
using FRMDesktopUI.Library.API;
using FRMDesktopUI.Library.Helpers;
using FRMDesktopUI.Library.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FRMDesktopUI.ViewModels
{
	public class SalesViewModel : Screen
	{
		private BindingList<ProductModel> _products;
		private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();
		private ProductModel _selectedProduct;
		private int _itemQuantity = 1;
		private IProductEndpoint _productEndpoint;
		private IConfigHelper _configHelper;

		public SalesViewModel (IProductEndpoint productEndpoint, IConfigHelper configHelper)
		{
			_productEndpoint = productEndpoint;
			_configHelper = configHelper;
		}

		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadProducts();
		}

		private async Task LoadProducts()
		{
			var productList = await _productEndpoint.GetAll();
			Products = new BindingList<ProductModel>(productList);
		}

		public ProductModel SelectedProduct
		{
			get { return _selectedProduct; }
			set 
			{ 
				_selectedProduct = value;
				NotifyOfPropertyChange(() => SelectedProduct);
				NotifyOfPropertyChange(() => CanAddToCart);
			}
		}

		public BindingList<ProductModel> Products
		{
			get { return _products; }
			set { 
				_products = value;
				NotifyOfPropertyChange(() => Products);
			}
		}

		public BindingList<CartItemModel> Cart
		{
			get { return _cart; }
			set
			{
				_cart = value;
				NotifyOfPropertyChange(() => Cart);
			}
		}

		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set 
			{
				_itemQuantity = value;
				NotifyOfPropertyChange(() => ItemQuantity);
				NotifyOfPropertyChange(() => CanAddToCart);
			}
		}

		public string SubTotal
		{
			get 
			{
				return CalculateSubTotal().ToString("C");
			}
		}

		private Decimal CalculateSubTotal()
		{
			decimal subTotal = 0;

			foreach (var item in Cart)
			{
				subTotal += (item.Product.RetailPrice * item.QuantityInCart);
			}

			return subTotal;
		}

		public string Tax
		{
			get
			{
				return CalculateTax().ToString("C");
			}
		}

		private Decimal CalculateTax()
		{
			decimal taxTotal = 0;
			decimal taxRate = _configHelper.GetTaxRate()/100;

			foreach (var item in Cart)
			{
				if (item.Product.IsTaxable)
				{
					taxTotal += (item.Product.RetailPrice * item.QuantityInCart * taxRate);
				}
			}

			return taxTotal;
		}

		public string Total
		{
			get
			{
				decimal total = CalculateSubTotal() + CalculateTax();
				return total.ToString("C");
			}
		}

		public bool CanAddToCart
		{
			get
			{
				bool output = false;

				// Make sure something is selected
				// Make sure there is item quantity
				if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
				{
					output = true;
				}

				return output;
			}
		}
		public void AddToCart()
		{ 
			CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

			if (existingItem != null)
			{
				existingItem.QuantityInCart += ItemQuantity;
				// HACK - There should be a better way of refreshing the cart display
				Cart.Remove(existingItem);
				Cart.Add(existingItem);
			}
			else
			{ 
				CartItemModel item = new CartItemModel
				{
					Product = SelectedProduct,
					QuantityInCart = ItemQuantity
				};
				Cart.Add(item);
			}

			SelectedProduct.QuantityInStock -= ItemQuantity;
			ItemQuantity = 1;
			NotifyOfPropertyChange(() => SubTotal);
			NotifyOfPropertyChange(() => Tax);
			NotifyOfPropertyChange(() => Total);
		}

		public bool CanRemoveToCart
		{
			get
			{
				bool output = false;

				// Make sure something is selected

				return output;
			}
		}

		public void RemoveFromCart()
		{
			NotifyOfPropertyChange(() => SubTotal);
			NotifyOfPropertyChange(() => Tax);
			NotifyOfPropertyChange(() => Total);
		}

		public bool CanCheckOut		
		{
			get
			{
				bool output = false;

				// Make sure something in the cart

				return output;
			}
		}

		public void CheckOut()
		{
			throw new NotImplementedException();
		}
	}
}
