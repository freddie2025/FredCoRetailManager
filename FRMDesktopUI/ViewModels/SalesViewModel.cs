﻿using Caliburn.Micro;
using FRMDesktopUI.Library.API;
using FRMDesktopUI.Library.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace FRMDesktopUI.ViewModels
{
	public class SalesViewModel : Screen
	{
		private BindingList<ProductModel> _products;
		private BindingList<ProductModel> _cart;
		private int _itemQuantity;
		private IProductEndpoint _productEndpoint;

		public SalesViewModel (IProductEndpoint productEndpoint)
		{
			_productEndpoint = productEndpoint;
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

		public BindingList<ProductModel> Products
		{
			get { return _products; }
			set { 
				_products = value;
				NotifyOfPropertyChange(() => Products);
			}
		}

		public BindingList<ProductModel> Cart
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
			}
		}

		public string SubTotal
		{
			get 
			{
				// TODO - Replace with calculation
				return "$0.00";
			}
		}

		public string Tax
		{
			get
			{
				// TODO - Replace with calculation
				return "$0.00";
			}
		}

		public string Total
		{
			get
			{
				// TODO - Replace with calculation
				return "$0.00";
			}
		}

		public bool CanAddToCart
		{
			get
			{
				bool output = false;

				// Make sure something is selected
				// Make sure there is item quantity

				return output;
			}
		}
		public void AddToCart()
		{
		
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

		}
	}
}
