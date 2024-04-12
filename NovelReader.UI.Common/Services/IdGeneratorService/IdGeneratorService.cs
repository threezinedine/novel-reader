﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelReader.UI.Common.Services.IdGeneratorService
{
	public class IdGeneratorService
	{
		public string GenerateId(int length)
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable.Repeat(chars, length)
								.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
