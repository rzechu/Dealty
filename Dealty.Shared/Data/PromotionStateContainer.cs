using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealty.Shared.Data
{
    public class PromotionStateContainer
    {
        public Promotion Value { get; set; }
        public event Action OnStateChange;

        public async void SetValue(Promotion value)
        {
            this.Value = value;
            await NotifyStateChanged();
        }

        private async Task NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
