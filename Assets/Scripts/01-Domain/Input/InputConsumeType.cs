namespace Domain {
    public enum InputConsumeType {
        NotHandled,   // layer ignored the event
        PassThrough,  // layer "saw" the event but allows it to continue
        Handled       // layer consumed it; stop propagation
    }
}