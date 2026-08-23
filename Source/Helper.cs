namespace Source
{
	public static class Helper
	{
		/// <summary>
		///		Subtracts the date of birth from today's date to calculate a person's age.
		/// </summary>
		/// <remarks>
		///		Source: <see href="https://stackoverflow.com/a/11942"></see>
		/// </remarks>
		/// <param name="birthdate">
		///		Birthdate formatted in yyyyMMdd. Example: <c>19992314</c>.
		///	</param>
		/// <returns>A person's age based on <paramref name="birthdate"/>.</returns>
		/// 
		public static int CalculateAge(string birthdate)
		{
			var age = (int.TryParse(DateTime.Now.ToString("yyyyMMdd"), out var today) && int.TryParse(birthdate, out var intBirthdate))
				? today - intBirthdate / 1000
				: 0;

			// Returns '0' if age is less or equal than 'zero'
			return age <= 0 ? 0 : age;
		}
	}
}
