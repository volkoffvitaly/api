﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Models
{
	public class TinkoffTokenEditModel
	{
		[Required]
		public string Token { get; set; }
	}
}
