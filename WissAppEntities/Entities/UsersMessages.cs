namespace WissAppEntities.Entities
{

    public partial class UsersMessages
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int? ReceiverId { get; set; }

        public int MessageId { get; set; }

        public virtual Messages Messages { get; set; }

        public virtual Users Sender { get; set; }

        public virtual Users Receiver { get; set; }
    }
}
