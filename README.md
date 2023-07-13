# shader-hlsl-test

wpf-shader置灰测试用例

## 原理解释：

#### 1.GrayScale

着色器代码 (GrayScale.ps) 的主要作用是将输入图像的每个像素转换为灰度值，并根据给定的 "amount" 值插值到原始颜色中，最终实现置灰效果。

1.读取像素的原始颜色。每个像素颜色具有红、绿和蓝分量，表示为 color.rgb：
```
float4 color = tex2D(input, uv);
```
2.计算像素的灰度值。我们使用概率加权求和方法，该方法根据人类对不同颜色的感知灵敏度为每个颜色分量分配权重：
```
float gray = dot(color.rgb, float3(0.299, 0.587, 0.114));
```
其中，权重值分别为：红色权重 0.299，绿色权重 0.587，蓝色权重 0.114。这些值来源于对颜色的感知特性进行量化衡量。

3.使用Linear Interpolation (lerp)函数进行插值。根据提供的 "amount" 值 (范围：0 到 1)，分量将从其原始颜色 (rgb) 缓和过渡到等效灰度值 (gray.xxx)：
```
color.rgb = lerp(color.rgb, gray.xxx, amount);
```

"lerp" 函数的基本插值公式：result = x + (y - x) * a，其中 ‘x’ 是初始值，‘y’ 是目标值，‘a’ 是插值摩擦系数（在我们的示例中，即为 "amount"）。

4.返回转换后的颜色值，以使置灰效果在整个图像上应用：

如此一来，根据提供的 "amount" 参数，该着色器将为输入图像生成适当的灰度图像。

#### 2.GrayFilter

自定义的 ColorProcessing 函数处理颜色，该函数接受四个参数：c（当前像素的颜色），h（色调[0, 360]），s（饱和度[-1, 1]），以及l（亮度[-1, 1]）

	1.定义两个 3x3 的 matrixH 和 matrixH2 矩阵。
 
	2.创建一个名为 rotateZ 的 3x3 旋转矩阵，这个矩阵用于旋转色调 h。
 
	3.将 matrixH 与 rotateZ 矩阵相乘，并将结果与 matrixH2 相乘，得到处理色调的最终矩阵。
 
	4.计算饱和度矩阵 matrixS。将原始饱和度值 s 与 (1 - s) 相乘，然后根据感知亮度权重分配给 RGB 通道。
 
	5.将色调矩阵和饱和度矩阵相乘，以应用色调和饱和度调整。
 
	6.将当前像素颜色与处理后矩阵相乘，获得调整后的颜色值 c1。
 
	7.将亮度 l 添加到调整后的颜色值 c1。
 
	8.使用 saturate 函数确保颜色分量在 0 到 1 的范围内。

设置合适的亮度，通过调整饱和度转换为灰度图像。

核心代码
```
float4 ColorProcessing(float4 c, float h, float s, float l)
{
    float3x3 matrixH =
    {
        0.8164966f, 0, 0.5352037f,
         -0.4082483f, 0.70710677f, 1.0548190f,
         -0.4082483f, -0.70710677f, 0.1420281f
    };

    float3x3 matrixH2 =
    {
        0.84630f, -0.37844f, -0.37844f,
         -0.37265f, 0.33446f, -1.07975f,
          0.57735f, 0.57735f, 0.57735f
    };

    float a = c.a;
    float3x3 rotateZ =
    {
        cos(radians(h)), sin(radians(h)), 0,
		-sin(radians(h)), cos(radians(h)), 0,
		0, 0, 1
    };
    matrixH = mul(matrixH, rotateZ);
    matrixH = mul(matrixH, matrixH2);

    float i = 1 - s;
    float3x3 matrixS =
    {
        i * 0.3086f + s, i * 0.3086f, i * 0.3086f,
		i * 0.6094f, i * 0.6094f + s, i * 0.6094f,
		i * 0.0820f, i * 0.0820f, i * 0.0820f + s
    };
    matrixH = mul(matrixH, matrixS);

    float3 c1 = mul(c, matrixH);
    c1 += l;
    return saturate(float4(c1, c.a));
}
```
