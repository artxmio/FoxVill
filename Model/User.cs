﻿using System.ComponentModel.DataAnnotations;

namespace FoxVill.Model;

public class User
{
    [Key]
    public string Email { get; set; } = "";

    public string Password { get; set; } = "";
}
