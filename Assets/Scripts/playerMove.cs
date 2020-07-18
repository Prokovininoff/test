using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour {

	//导入CharacterController函数到“controller”
	public CharacterController controller;

	// 定义公共变量 - 移动速度，重力加速度，跳跃高度
	public float speed = 12f;
	public float g = -18.62f;
	public float jumpHeight = 3f;

	// 用于定义落地判定的图层的变量
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;

	Vector3 velocity;
	bool isGrounded;

	// 每帧刷新 - 执行以下命令
	void Update () {
		// 检查是否着地
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		// 如果着地，留在地面 = 垂直速度为常量
		if (isGrounded && velocity.y < 0){
			velocity.y = -2f;
		}

		// 检测移动按键 (1/-1) (WA/SD)
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		// 将 x/z 轴的移动存入向量
		Vector3 move = transform.right * x + transform.forward * z;
		// 通过移动向量，速度和时间执行移动
		controller.Move(move * speed * Time.deltaTime);

		// 跳跃判定
		if (Input.GetButtonDown("Jump") && isGrounded) {
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * g);
		}

		// 计算自由落体速度
		velocity.y += g * Time.deltaTime;
		// y轴移动
		controller.Move(velocity * Time.deltaTime);
	}
}
