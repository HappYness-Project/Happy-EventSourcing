using System;


namespace HP.Domain
{
    public record Email
    {
        public Email(string emailAddr)
        {
            if (string.IsNullOrWhiteSpace(emailAddr))
                throw new ArgumentNullException(nameof(emailAddr));

            if (!emailAddr.Contains('@'))
                throw new ArgumentException($"Invalid email address: {EmailAddr}", nameof(EmailAddr));

            EmailAddr = emailAddr;
        }

        public string EmailAddr { get; }
        public override string ToString() => this.EmailAddr;
    }
}
