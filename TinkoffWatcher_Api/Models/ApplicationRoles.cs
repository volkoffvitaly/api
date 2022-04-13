using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinkoffWatcher_Api.Models
{
	public static class ApplicationRoles
	{
		public const String Administrators = nameof(ApplicationRoles.Administrators);
		public const String Users = nameof(ApplicationRoles.Users);
		public const String Student = nameof(ApplicationRoles.Student);
		public const String Curator = nameof(ApplicationRoles.Curator);
		public const String RepresentativeCompany = nameof(ApplicationRoles.RepresentativeCompany);
		public const String RepresentativeSchool = nameof(ApplicationRoles.RepresentativeSchool);

	}
}
