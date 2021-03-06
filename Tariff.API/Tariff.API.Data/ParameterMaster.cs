﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tariff.API.Contracts.Data;

namespace Tariff.API.Data
{
    public class ParameterMaster:IParameterMaster
    {
        //Model for ParameterMaster Table in Database

        [Key]
        //Primary Key
        public int parameterId { get; set; }

        [Required(ErrorMessage = "Enter name")]
        //regular expression for match only alphabets
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Enter only words please")]
        public string parameterName { get; set; }

        [Required]
        public byte isActive { get; set; }

        public ICollection<RuleDetails> ruleDetails { get; set; }
    }
}
