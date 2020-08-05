using System.Linq;

namespace DAN_L_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Converts the input values to another needed in the models
    /// </summary>
    class Validations
    {
        /// <summary>
        /// Valid positive int input
        /// </summary>
        public string ValidPositiveNumber(int? number, int maxNumber)
        {
            if (number < 0 || number > maxNumber)
            {
                return "The number cannot be bigger than " + maxNumber;
            }
            return null;
        }

        /// <summary>
        /// Checks if the password is correct
        /// </summary>
        /// <param name="password">the password we are checking</param>
        /// <returns>jmbg if the input is correct or null if its wrong</returns>
        public string PasswordChecker(string password)
        {
            int numberOfUpperCharacters = 0;

            if (password == null || !(password.Length < 6))
            {
                return null;
            }

            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsUpper(password[i]))
                {
                    numberOfUpperCharacters++;
                    if(numberOfUpperCharacters == 2)
                    {
                        break;
                    }
                }
            }

            if (numberOfUpperCharacters < 2)
            {
                return null;
            }

            return password;
        }
    }
}
