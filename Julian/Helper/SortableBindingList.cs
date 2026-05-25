using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Helper
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool isSorted;
        private ListSortDirection sortDirection;
        private PropertyDescriptor sortProperty;

        public SortableBindingList(List<T> list) : base(list) { }

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => isSorted;
        protected override ListSortDirection SortDirectionCore => sortDirection;
        protected override PropertyDescriptor SortPropertyCore => sortProperty;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            var list = (List<T>)Items;

            // toggle sort nếu cùng column
            if (sortProperty == prop)
            {
                direction = sortDirection == ListSortDirection.Ascending
                    ? ListSortDirection.Descending
                    : ListSortDirection.Ascending;
            }

            list.Sort((x, y) =>
            {
                var xValue = prop.GetValue(x);
                var yValue = prop.GetValue(y);

                if (xValue == null) return -1;
                if (yValue == null) return 1;

                int result = Comparer<object>.Default.Compare(xValue, yValue);
                return direction == ListSortDirection.Ascending ? result : -result;
            });

            sortDirection = direction;
            sortProperty = prop;
            isSorted = true;

            ResetBindings();
        }
    }
}
