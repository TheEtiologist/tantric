float4x4 World;
float4x4 View;
float4x4 Projection;

float3 InfluencePoints[3];
float2 CameraPos;
float Timer : TIME;

sampler2D BS : TEXTURE0;
sampler2D Static : TEXTURE1;

struct VertexToPixel
{
    float4 Position   	: POSITION;    
    float2 texCoord		: TEXCOORD0;
};

struct PixelToFrame
{
    float4 Color : COLOR0;
};

VertexToPixel VertexShaderFunction( float4 inPos : POSITION, float2 texCoord: TEXCOORD0)
{	
	VertexToPixel Output = (VertexToPixel)0;
	
	Output.Position = inPos;
    Output.texCoord = float2(texCoord.x + sin(cos(Timer)), texCoord.y + sin(cos(Timer)));
	return Output;    
}

float4 PixelShaderFunction(float2 texCoord: TEXCOORD0) : COLOR0
{
	float total = 0;
	float d;
	float dx;
	float dy;
	bool point = false;
	float closestd = 1000;
	for( int m = 0; m < 3; m++ )
	{
		float3 TransPoint = InfluencePoints[m] - float3(CameraPos.x / 500, CameraPos.y / 500, 0);
		dy = abs(TransPoint.y - texCoord.y);
		dx = abs(TransPoint.x - texCoord.x);
		d = dy*dy + dx*dx;
		if( d < closestd )
			closestd = d;
		if( d < 0.0001f )
			point = true;
		if( TransPoint.z < 0 )
			d = 1/(d * d)/2;
		else
			d = 1.5/d;
		total += d * TransPoint.z;
	}
	if( point )
		return float4(1,0,0,.5);
	else if( total >= 20)
	{
		float Col = 15000 / (total * total * total );
		float col2 = Col * tex2D(BS, float2( texCoord.x + sin(texCoord.y*cos(Timer/5500000)), sin(cos(texCoord.y) + sin(closestd * Timer/5500000)) + texCoord.x*sin(Timer/5500000) * cos(closestd * Timer/5500000)) );
		return float4(col2, col2, col2, 1.3-Col);
	}
    else
		return float4(0,0,0,0);
}

technique Technique1
{
    pass Pass1
    {
	    AlphaBlendEnable=true;
		SrcBlend=srcalpha;
		AddressU[0] = Wrap;
		AddressV[0] = Wrap;
		//VertexShader = compile vs_2x VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
