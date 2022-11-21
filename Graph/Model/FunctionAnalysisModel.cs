using Graph.Core;

namespace Graph.Model
{
    internal class FunctionAnalysisModel : ObservableObject
    {
		internal void Clear()
		{
			ScopeOfValues = null;
			DefinitionScope = null;
			Maximum = null;
			Minimum = null;
			IntersWithAxisY = null;
			ZerosOfFunc = null;
			Parity = null;
		}

		private string _scopeOfValues;//обл знач E(y)
		public string ScopeOfValues
		{
			get => _scopeOfValues;
			set
			{
				_scopeOfValues = value;
				OnPropertyChanged();
			}
		}

		private string _definitionScope;//обл опред D(y)
		public string DefinitionScope
		{
			get => _definitionScope;
			set
			{
				_definitionScope = value;
				OnPropertyChanged();
			}
		}

		private string _maximum;
		public string Maximum
		{
			get => _maximum;
			set
			{
				_maximum = value;
				OnPropertyChanged();
			}
		}

		private string _minimum;
		public string Minimum
		{
			get => _minimum;
			set
			{
				_minimum = value;
				OnPropertyChanged();
			}
		}

		private string _intersWithAxisY;
		public string IntersWithAxisY
		{
			get => _intersWithAxisY;
			set
			{
				_intersWithAxisY = value;
				OnPropertyChanged();
			}
		}

		private string _zerosOfFunc;
		public string ZerosOfFunc
		{
			get => _zerosOfFunc;
			set
			{
				_zerosOfFunc = value;
				OnPropertyChanged();
			}
		}

		private string _parity;//чётность
		public string Parity
		{
			get => _parity;
			set
			{
				_parity = value;
				OnPropertyChanged();
			}
		}
	}
}