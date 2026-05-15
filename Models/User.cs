namespace Avatar_project.Models
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username used for login.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// Note: Storing passwords in plain text is used here for educational purposes only.
        /// In a real-world application, passwords must always be securely hashed.
        /// </summary>
        public string Password { get; set; }
    }
}
