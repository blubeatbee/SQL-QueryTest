namespace Source.Menu.UI
{
	public static class Input
	{
		private static Dictionary<Type, Delegate> isConvertableLookup = new();

		/// <summary>
		///     Prompts for user input, then loops until input is a non-empty <see cref="string"/>
		/// </summary>
		/// <remarks>
		///     <para>User may cancel by pressing <see cref="ConsoleKey.Escape"/>.</para>
		/// </remarks>
		/// <returns>
		///      <list type="table">
		///         <item><description>Returns a <see cref="string"/> that is NOT <see langword="null"/>, or <see cref="string.Empty"/>, or contains only whitespace characters, and without any leading or trailing whitespace characters</description></item>
		///         <item><description>If  is pressed, returns <see langword="null"/> instead</description></item>
		///     </list>
		/// </returns>
		/// <param name="prompt">Custom prompt text that instructs what user should include and exclude in their input</param>
		/// <param name="minLength">Minimum character length.</param>
		/// <param name="maxLength">Maximum character length.</param>
		/// <typeparam name="TType">
		///		Checks if user input is convertable into <typeparamref name="TType"/>. If an input is unable to be converted, it will be ignored.
		///		<para>
		///			NOTE: this method does not actually convert input to <typeparamref name="TType"/>.
		///		</para>
		///	</typeparam>
		public static string? ToString<TType>(string prompt, int minLength, int maxLength)
		{
			string inputs = string.Empty;

			while (true)
			{
				Console.Clear();
				Console.WriteLine(prompt);
				Console.WriteLine($"Input must be between {minLength} and {maxLength} characters long. Press ESC to cancel.");
				Console.WriteLine(inputs);

				ConsoleKeyInfo userInput = Console.ReadKey();

				if (userInput.Key == ConsoleKey.Enter)
				{
					inputs = inputs.Trim();

					if (string.IsNullOrWhiteSpace(inputs))
					{
						Console.WriteLine($"Input cannot be empty.");
						continue;
					}
					if (inputs.Length > maxLength || inputs.Length < minLength)
					{
						Console.WriteLine($"Input must be between {minLength} and {maxLength} characters long.");
						continue;
					}
					return inputs;
				}
				if (userInput.Key == ConsoleKey.Escape)
				{
					return null;
				}
				if (userInput.Key == ConsoleKey.Backspace && inputs.Length >= 1)
				{
					inputs = inputs.Remove(inputs.Length - 1);
					Console.Write("\b \b");
				}
				if (inputs.Length >= maxLength)
					continue;
				if (char.IsControl(userInput.KeyChar))
					continue;
				if (!Input.Validate<TType>(userInput.KeyChar.ToString()))
					continue;

				inputs += userInput.KeyChar;
			}
		}

		private static bool Validate<T>(string userInput)
		{
			if (Input.isConvertableLookup.Count == 0)
			{
				Input.RegisterConvertableTable();
			}

			var validator = Input.isConvertableLookup.GetValueOrDefault(typeof(T));

			return validator == null ? true : (bool?)validator.DynamicInvoke(userInput) ?? true;
		}

		private static void RegisterConvertableTable()
		{
			Input.isConvertableLookup.Add(typeof(sbyte), ConvertableToSByte);
			Input.isConvertableLookup.Add(typeof(short), ConvertableToShort);
			Input.isConvertableLookup.Add(typeof(int), ConvertableToInt);
			Input.isConvertableLookup.Add(typeof(long), ConvertableToLong);
		}

		private static bool ConvertableToSByte(string userInput)
		{
			return SByte.TryParse(userInput, out sbyte _);
		}
		private static bool ConvertableToShort(string userInput)
		{
			return Int16.TryParse(userInput, out short _);
		}
		private static bool ConvertableToInt(string userInput)
		{
			return Int32.TryParse(userInput, out int _);
		}
		private static bool ConvertableToLong(string userInput)
		{
			return Int64.TryParse(userInput, out long _);
		}
	}
}
