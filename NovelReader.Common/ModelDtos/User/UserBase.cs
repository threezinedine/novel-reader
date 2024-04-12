namespace NovelReader.Common.ModelDtos.User
{
	public enum Role
	{
		Admin,
		User,
	}

	public enum Gender
	{
		Male,
		Female,
		Other,
	}

	public class UserBase
	{
		public string Id { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public Gender Gender { get; set; } = Gender.Male;
		public string PhoneNumber { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string AvatarURL { get; set; } = string.Empty;
		public Role Role { get; set; } = Role.User;

	}
}
