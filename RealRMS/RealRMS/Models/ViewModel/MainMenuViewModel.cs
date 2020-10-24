namespace RealRMS.Models.ViewModel {
    public class MainMenuViewModel {
        public bool IsAdmin { get; set; }

        public bool IsCook { get; set; }

        public bool IsServer { get; set; }

        public bool IsRunner { get; set; }

        public bool IsClockedIn { get; set; }

        public int UserId { get; set; }

        public int TimeCardId { get; set; }
    }
}