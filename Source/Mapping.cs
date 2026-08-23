namespace Source
{
	/// <summary>
	///		Contains reusable methods for mapping objects into a selected DTO.
	/// </summary>
	/// <remarks>
	///		Source <see href="https://stackoverflow.com/a/62539083"/>
	/// </remarks>
	public static class Mapping
	{
		/// <summary>
		///		Maps object properties to a DTO.
		///		Will only work if <paramref name="model"/> and <paramref name="dto"/> have properties with the same name and type. 
		/// </summary>
		/// <remarks>
		///		<example>
		///		How to use method:
		///		<code>
		///			var myDtoObject = Mapping.ToDto(myModel, new MyDtoClass(),);
		///		</code>
		///		</example>
		/// </remarks>
		/// <typeparam name="T">The DTO <see langword="class"/> to map to.</typeparam>
		/// <param name="model">An <see langword="object"/> that will have its properties mapped to <paramref name="dto"/>.</param>
		/// <param name="dto">A <see langword="new"/> instance of <typeparamref name="T"/>.</param>
		/// <returns>A <typeparamref name="T"/> <see langword="object"/> where <typeparamref name="T"/> is the DTO.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static T ToDto<T>(object model, T dto)
		{
			var dtoProps = (dto != null)
				? dto.GetType().GetProperties()
				: throw new ArgumentNullException(nameof(dto));
			var modelProps = (model != null)
				? model.GetType().GetProperties()
				: throw new ArgumentNullException(nameof(model));

			dtoProps.ToList().ForEach(d =>
			{
				var modelP = modelProps.FirstOrDefault(m => m.Name == d.Name && m.PropertyType == d.PropertyType);
				if (modelP != null)
				{
					d.SetValue(dto, modelP.GetValue(model));
				}
			});

			return dto;
		}

		/// <summary>
		///		<inheritdoc cref="ToDto{T}(object, T)"/>
		/// </summary>
		/// <remarks>
		///		<example>
		///		How to use method:
		///		<code>
		/// 		var myModelDto = Mapping.ToDto(myModel1, myModel2, new ModelDto());
		///		</code>
		///		</example>
		/// </remarks>
		/// <typeparam name="T"><inheritdoc cref="ToDto{T}(object, T)"/></typeparam>
		/// <param name="model1">An <see langword="object"/> that will have its properties mapped to <paramref name="dto"/>.</param>
		/// <param name="model2">An <see langword="object"/> that will have its properties mapped to <paramref name="dto"/>.</param>
		/// <param name="dto">A <see langword="new"/> instance of <typeparamref name="T"/>.</param>
		/// <returns><inheritdoc cref="ToDto{T}(object, T)"/></returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static T ToDto<T>(object model1, object model2, T dto)
		{
			var dtoProps = (dto != null) ? dto.GetType().GetProperties()
				: throw new ArgumentNullException(nameof(dto));
			var model1Props = (model1 != null) ? model1.GetType().GetProperties()
				: throw new ArgumentNullException(nameof(model1));
			var model2Props = (model2 != null) ? model2.GetType().GetProperties()
				: throw new ArgumentNullException(nameof(model2));

			dtoProps.ToList().ForEach(d =>
			{
				var model1P = model1Props.FirstOrDefault(m1 => m1.Name == d.Name && m1.PropertyType == d.PropertyType);
				var model2P = model2Props.FirstOrDefault(m2 => m2.Name == d.Name && m2.PropertyType == d.PropertyType);

				if (model1P != null)
				{
					d.SetValue(dto, model1P.GetValue(model1));
				}
				else if (model2P != null)
				{
					d.SetValue(dto, model2P.GetValue(model2));
				}
			});
			return dto;
		}
	}
}
