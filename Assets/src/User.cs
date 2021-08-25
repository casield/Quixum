public class User {
    public Player2 player;
    public UserState userState;

    public User(UserState userState){
        this.userState = userState;
    }

    public void AddPlayer(Player2 player){
        this.player = player;
    }
}