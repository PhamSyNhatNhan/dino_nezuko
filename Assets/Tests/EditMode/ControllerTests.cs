using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ControllerTests
{
    private Controller controller;
    private GameManager gameManager;

    private GameObject groundObject;

    [SetUp]
    public void Setup()
    {
        // Tạo Controller
        GameObject controllerObject = new GameObject("Player");
        controller = controllerObject.AddComponent<Controller>();

        // Thiết lập GroundCheck cho Controller
        controller.GroundCheck = controllerObject.transform; 
        controller.GroundCheckRadius = 0.1f; 
        controller.WhatisGround = LayerMask.GetMask("Ground"); 

        // Thêm Rigidbody2D cho Controller
        Rigidbody2D rb = controllerObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 5f; 

        // Tạo GameManager
        GameObject gmObject = new GameObject("GameManager");
        gameManager = gmObject.AddComponent<GameManager>();

        // Tạo Ground
        groundObject = new GameObject("subGround");
        groundObject.layer = LayerMask.NameToLayer("Ground"); 
        groundObject.AddComponent<BoxCollider2D>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(controller.gameObject);
        Object.DestroyImmediate(gameManager.gameObject);
        Object.DestroyImmediate(groundObject);
    }

    [Test]
    public void Slash_ChangesScale()
    {
        // Act
        controller.getslash();
        // Assert
        Assert.AreEqual(new Vector3(0.5f, 0.5f, 0.5f), controller.transform.localScale);
    }

    [Test]
    public void ResetSlash_RestoresScale()
    {
        // Arrange
        controller.getslash();
        // Act
        controller.getresetSlash();
        // Assert
        Assert.AreEqual(Vector3.one, controller.transform.localScale);
    }

    [Test]
    public void CheckGround_OnGround()
    {
        controller.transform.position  = groundObject.transform.position + new Vector3(0, controller.GroundCheckRadius, 0); 
        
        // Act
        controller.getcheckGround();

        // Assert
        Assert.IsTrue(controller.getisGrounded());
    }

    [Test]
    public void CheckGround_NotOnGround()
    {
        // Arrange
        controller.transform.position = groundObject.transform.position + new Vector3(0, controller.GroundCheckRadius + 1f, 0);
        
        // Act
        controller.getcheckGround();

        // Assert
        Assert.IsFalse(controller.getisGrounded());
    }
    
}
