using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_System.Models
{
    internal class Card
    {
        public int Id { get; set; }
        public Guid CardNumber { get; set; } = Guid.NewGuid();

        public Card()
        {
        }

        public Card(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("The card ID must be a positive integer.");
            }

            Id = id;
            CardNumber = Guid.NewGuid();
        }
    }
}