using System.ComponentModel;

namespace FRMDesktopUI.Models
{
	public class CartItemDisplayModel : INotifyPropertyChanged
	{
		public ProductDisplayModel Product { get; set; }

		private int _quantityInCart;
		public int QuantityInCart 
		{
			get { return _quantityInCart; }
			set 
			{ 
				_quantityInCart = value;
				CallPropertyChnaged(nameof(QuantityInCart));
				CallPropertyChnaged(nameof(DisplayText));
			}
		}

		public string DisplayText
		{
			get
			{
				return $"{ Product.ProductName } ({ QuantityInCart })";
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void CallPropertyChnaged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
