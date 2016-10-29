using System;

namespace Chatty.BLL.Entities
{
    [Serializable]
    public class Message
    {
        public Message(int userId, int roomId, string content)
        {
            UserId = userId;
            RoomId = roomId;
            Content = content;
        }

        public int UserId { get; set; }
        public int RoomId { get; set; }
        public string Content { get; set; }
        
    }
}
