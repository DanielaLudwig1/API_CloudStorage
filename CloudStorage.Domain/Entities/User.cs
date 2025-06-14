﻿namespace CloudStorage.Domain.Entities
{
    public class User
    {
        //DADOS QUE SERÃO USADOS NA REQUISIÇÃO DO USUÁRIO
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

    }
}
