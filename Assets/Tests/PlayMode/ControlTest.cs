using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ControlTest
{
    private GameManager gm;
    private Controller ctl;
    private Rigidbody2D rb;
    
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Tải scene
        yield return SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);

        // Đảm bảo scene đã được tải
        Assert.AreEqual("SampleScene", SceneManager.GetActiveScene().name);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ctl = GameObject.Find("Player").GetComponent<Controller>();
        rb = ctl.GetComponent<Rigidbody2D>();
    }
    
    [TearDown]
    public void TearDown()
    {
        // Dọn dẹp sau mỗi test
        Object.Destroy(ctl.gameObject);
        Object.Destroy(gm.gameObject);
    }
    
    [UnityTest]
    public IEnumerator JumpTest()
    {
        gm.newGame();
        ctl.isJumpCalled = false;
        
        yield return new WaitForSeconds(0.5f);
        ctl.getjump();
        
        yield return new WaitForSeconds(0.1f);
        
        Assert.IsTrue(ctl.isJumpCalled);
    }
    
    private float totalTime = 2f;  // Tổng thời gian mô phỏng
    private float timeElapsed = 0f;
    private bool isGameStarted = false;

    [UnityTest]
    public IEnumerator SmolTest()
    {
        // Khởi động lại game
        gm.newGame();

        // Mô phỏng Update()
        yield return new WaitForSeconds(0.5f);
        ctl.getslash();
        yield return null;
        Assert.AreNotEqual(ctl.transform.localScale, new Vector3(1f,1f,1f));
        ctl.CanInput = false;
        ctl.IsSlash = true;
        yield return new WaitForSeconds(0.2f);
        Assert.AreEqual(ctl.transform.localScale, new Vector3(1f,1f,1f));
    }
}
