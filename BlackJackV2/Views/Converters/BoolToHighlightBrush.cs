using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackV2.Views.Converters
{
	public class BoolToHighlightBrush : IValueConverter
	{
		public IBrush ActiveBrush { get; set; } = Brushes.Goldenrod;
		public IBrush InactiveBrush { get; set; } = Brushes.Transparent;

		/**
		 * Converts a boolean value to visible or transparant brush.
		 **/

		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			// Check if the value is a boolean
			// Then if it is true, return the active brush
			return value is bool isActive && isActive ? ActiveBrush : InactiveBrush;
		}


		// Not used
		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
