using System;
using System.ComponentModel.DataAnnotations;

namespace Neddle.Validation
{
    /// <summary>
    /// Contains methods for validating <see cref="Guid"/> instances.
    /// </summary>
    public static class GuidValidator
    {
        /// <summary>
        /// Determines whether the specified <see cref="Guid"/> is equal to Guid.Empty.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="ValidationResult"/> containing results of the validation.</returns>
        public static ValidationResult IsNotEmpty(Guid obj, ValidationContext context)
        {
            if (obj == Guid.Empty)
            {
                return new ValidationResult(Resources.Validation.GuidCannotBeEmpty);
            }

            return ValidationResult.Success;
        }
    }
}