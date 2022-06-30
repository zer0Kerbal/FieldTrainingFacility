﻿//2
// 
// This code was generated by a tool. Any changes made manually will be lost
// the next time this code is regenerated.
// 
/* Field Training Lab (FTL)
 * This addon adds a training center in the science laboratory. Paying science points gets kerbals experience. For Kerbal Space Program.
 * Copyright (C) 2016 EFour
 * Copyright (C) 2019, 2022 zer0Kerbal (zer0Kerbal at hotmail dot com)
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/


using System.Reflection;

[assembly: AssemblyFileVersion("1.2.1.2")]
[assembly: AssemblyVersion("1.2.1.0")]
[assembly: AssemblyInformationalVersion("1.2.1")]
[assembly: KSPAssembly("FieldTrainingFacility", 1,2,1)]

namespace FieldTrainingFacility
{
	public static class Version
	{
		public const int major = 1;
		public const int minor = 2;
		public const int patch = 1;
		public const int build = 0;
		public const string Number = "1.2.1.0";
#if DEBUG
        public const string Text = Number + "-zed'K BETA DEBUG";
        public const string SText = Number + "-zed'K BETA DEBUG";
#else
        public const string Text = Number + "-zed'K";
		public const string SText = Number;
#endif
	}
}