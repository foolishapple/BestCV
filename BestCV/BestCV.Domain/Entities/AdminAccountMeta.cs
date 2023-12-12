using Jobi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobi.Domain.Entities
{
	public class AdminAccountMeta : EntityCommon<int>
	{
		public long AdminAccountId { get; set; }
		public string Key { get; set; } = null!;
		public string Value { get; set; } = null!;

		public virtual AdminAccount AdminAccount { get; } = null!;
	}
}
