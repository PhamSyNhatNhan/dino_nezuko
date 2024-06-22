using System.Collections;
using NUnit.Framework;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayTest
{
    private Controller ctl;
    private GameManager gm;
    private Rigidbody2D rb;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
        Assert.AreEqual("SampleScene", SceneManager.GetActiveScene().name);

        // Tìm và lấy các thành phần cần thiết
        ctl = GameObject.Find("Player").GetComponent<Controller>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = ctl.GetComponent<Rigidbody2D>();
    }

    [UnityTearDown]
    public void TearDown()
    {
        // Dọn dẹp các thành phần đã khởi tạo
        Object.Destroy(ctl.gameObject);
        Object.Destroy(gm.gameObject);
    }

    [UnityTest]
    public IEnumerator GameOverTest()
    {
        gm.newGame();
        gm.generateEnemy();
        yield return null;
        
        Assert.IsFalse(gm.IsGameStart);
    }

    [UnityTest]
    public IEnumerator NewGameTest()
    {
        // Khởi động lại game
        gm.newGame();
        
        yield return null;

        Assert.IsTrue(gm.IsGameStart);
    }
    
    [UnityTest]
    public IEnumerator EnemyTest()
    {
        // Khởi động lại game
        gm.newGame();
        
        yield return new WaitForSeconds(1f);

        Assert.AreNotEqual(gm.EnemyList.Count, 0);
    }
    
    [UnityTest]
    public IEnumerator EnemyMoveTest()
    {
        // Khởi động lại game
        gm.newGame();
        
        yield return new WaitForSeconds(1f);

        Assert.Less(gm.EnemyList[0].GetComponent<Rigidbody2D>().velocityX, 0);
    }
}