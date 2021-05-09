<?php 

class PDOConn {

    public static $dbname = "PracticaFinal";
        
    private static $serverName = 'localhost';

    private static $userName = 'postgres';

    private static $password = '1234';

    public static $conn;

    public static function connect()
    {
        try {
            self::$conn = new PDO('pgsql:host='.self::$serverName.';port=5433;dbname='.self::$dbname,self::$userName,self::$password);
            self::$conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
            //echo "Connected successfully";
        }
        catch (PDOException $e) {
            echo "Connection failed: " . $e->getMessage();
        }
        return(self::$conn);
    }


    public static function disconnect()
    {
        self::$conn = null;
        //echo "Disconnected succesfully";
    }
}