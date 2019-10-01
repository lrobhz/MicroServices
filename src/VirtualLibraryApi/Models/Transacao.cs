using System;

namespace VirtualLibraryApi.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public long UsuarioId { get; set; }
        public long PedidoId { get; set; }
    }
}