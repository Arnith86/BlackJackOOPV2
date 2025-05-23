using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using BlackJackV2.Models.Card;
using DynamicData.Binding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Views.Converters
{
	public class ElementToIndexConverter : IMultiValueConverter
	{

		public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
		{
			if (values.Count >= 2)
			{
				string one = ($"Value[0] Type: {values[0]?.GetType().Name} - {values[0]}");
				string two = ($"Value[1] Type: {values[1]?.GetType().Name} - {values[1]}");
			}
			//return values[0] is IObservableCollection<ICard<Bitmap, string>> collection ? collection.IndexOf(values[1]) : -1;

			// Check if the values are of correct type, and if so,
			// return the index of the item in the collection
			if (values.Count >= 2 && 
				values[0] is IEnumerable<ICard<Bitmap, string>> collection && 
				values[1] is ICard<Bitmap, string> item)
			{
				return collection.ToList().IndexOf(item);
			}

			// something went wrong
			return -1;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return Array.Empty<object>();
		}
	}
	
}
