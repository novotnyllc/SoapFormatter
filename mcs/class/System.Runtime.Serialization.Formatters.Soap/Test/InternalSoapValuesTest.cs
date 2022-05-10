using System;
using System.IO;
using NUnit.Framework;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization;

namespace SoapShared
{
	/// <summary>
	/// Summary description for InternalSoapValuesTest.
	/// </summary>
	[TestFixture]
	public class InternalSoapValuesTest
	{
		private MemoryStream ms;
		private SoapFormatter sf;
		

		public InternalSoapValuesTest()
		{
			ms = new MemoryStream();
			sf = new SoapFormatter();
		}
		[Test]
		public void WriteDateTimeData()
		{
			object obj = new SerializableClass();
			ms = new MemoryStream();
			Serialize(obj, ms);
			ms.Position = 0;
			var r = new System.IO.StreamReader(ms);
			string s = r.ReadToEnd();
			TestContext.WriteLine(s);
			DateTime expectedTime = DateTime.Parse("2022-05-07T10:40:46.0618350-04:00");

			//test needs to expect the date to be formatted in the datetime of the local system
			string expected =
$@"<SOAP-ENV:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:clr=""http://schemas.microsoft.com/clr/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
  <SOAP-ENV:Body>
    <a1:SerializableClass id=""ref-1"" xmlns:a1=""http://schemas.microsoft.com/clr/nsassem/SoapShared/Test%2C%20Version%3D1.0.0.0%2C%20Culture%3Dneutral%2C%20PublicKeyToken%3D64d05efcff27afd3"">
      <m_time xsi:type=""xsd:dateTime"">{expectedTime.ToLocalTime():yyyy-MM-ddTHH:mm:ss.fffffffzzz}</m_time>
    </a1:SerializableClass>
  </SOAP-ENV:Body>
</SOAP-ENV:Envelope>";
			Assert.AreEqual(expected, s);
		}

		[Test]
		public void ReadDateTimeData()
		{
			string soapMessage =
@"<SOAP-ENV:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:clr=""http://schemas.microsoft.com/clr/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
  <SOAP-ENV:Body>
    <a1:SerializableClass id=""ref-1"" xmlns:a1=""http://schemas.microsoft.com/clr/nsassem/SoapShared/Test%2C%20Version%3D1.0.0.0%2C%20Culture%3Dneutral%2C%20PublicKeyToken%3D64d05efcff27afd3"">
      <m_time xsi:type=""xsd:dateTime"">2022-05-07T10:40:46.0618350-04:00</m_time>
    </a1:SerializableClass>
  </SOAP-ENV:Body>
</SOAP-ENV:Envelope>";

			ms = new MemoryStream(System.Text.UTF8Encoding.Default.GetBytes(soapMessage));
			ms.Position = 0;
			var obj = Deserialize(ms) as SerializableClass;
			
			DateTime expectedTime = DateTime.Parse("2022-05-07T10:40:46.0618350-04:00");
			Assert.AreEqual(expectedTime, obj.Time);
		}

		[Test]
		public void WriteTimeSpanData()
		{
			object obj = new Serializable2Class();
			ms = new MemoryStream();
			Serialize(obj, ms);
			ms.Position = 0;
			var r = new System.IO.StreamReader(ms);
			string s = r.ReadToEnd(); 
			TestContext.WriteLine(s);
			string expected =
@"<SOAP-ENV:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:clr=""http://schemas.microsoft.com/clr/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
  <SOAP-ENV:Body>
    <a1:Serializable2Class id=""ref-1"" xmlns:a1=""http://schemas.microsoft.com/clr/nsassem/SoapShared/Test%2C%20Version%3D1.0.0.0%2C%20Culture%3Dneutral%2C%20PublicKeyToken%3D64d05efcff27afd3"">
      <m_time xsi:type=""xsd:duration"">P1DT</m_time>
    </a1:Serializable2Class>
  </SOAP-ENV:Body>
</SOAP-ENV:Envelope>";
			Assert.AreEqual(expected, s);
		}

		[Test]
		public void ReadTimeSpanData()
		{
			string soapMessage =
@"<SOAP-ENV:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/"" xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:clr=""http://schemas.microsoft.com/clr/"" SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
  <SOAP-ENV:Body>
    <a1:Serializable2Class id=""ref-1"" xmlns:a1=""http://schemas.microsoft.com/clr/nsassem/SoapShared/Test%2C%20Version%3D1.0.0.0%2C%20Culture%3Dneutral%2C%20PublicKeyToken%3D64d05efcff27afd3"">
      <m_time xsi:type=""xsd:duration"">P1DT</m_time>
    </a1:Serializable2Class>
  </SOAP-ENV:Body>
</SOAP-ENV:Envelope>";

			ms = new MemoryStream(System.Text.UTF8Encoding.Default.GetBytes(soapMessage));
			ms.Position = 0;
			var obj = Deserialize(ms) as Serializable2Class;

			Assert.AreEqual(new TimeSpan(TimeSpan.TicksPerDay), obj.Time);
		}

		[Test]
		public void WriteReadData()
		{
			SerializedClass c = new SerializedClass();

			SerializeDeserialize(c);
			SerializeDeserialize(new SerializedClass[]{c,c});
			SerializeDeserialize(c.str);
			SerializeDeserialize(c.m_bool);
			SerializeDeserialize(c.m_byte);
			SerializeDeserialize(c.m_bytes);
			SerializeDeserialize(c.m_decimal);
			SerializeDeserialize(c.m_double);
			SerializeDeserialize(c.m_float);
			SerializeDeserialize(c.m_int);
			SerializeDeserialize(c.m_long);
			SerializeDeserialize(c.m_object);
			SerializeDeserialize(c.m_sbyte);
			SerializeDeserialize(c.m_short);
			SerializeDeserialize(c.m_timeSpan);
			SerializeDeserialize(c.m_time);
			SerializeDeserialize(c.m_uint);
			SerializeDeserialize(c.m_ulong);
			SerializeDeserialize(c.m_ushort);
		}

		private void SerializeDeserialize(object obj)
		{
			ms = new MemoryStream();
			Serialize(obj, ms);
			ms.Position = 0;
			Object des = Deserialize(ms);
			Assert.AreEqual (obj.GetType(), des.GetType());
		}

		private void Serialize(object ob, Stream stream)
		{
			sf.Serialize(stream, ob);
		}

		private object Deserialize(Stream stream)
		{
			Object obj = sf.Deserialize(stream);
			return obj;
		}
	}
	
	[Serializable]
	class SerializedClass
	{
		public string str = "rrr";
		public bool m_bool;
		public sbyte m_sbyte;
		public byte m_byte;
		public long m_long;
		public ulong m_ulong;
		public int m_int;
		public uint m_uint;
		public float m_float;
		public double m_double;
		public decimal m_decimal;
		public short m_short;
		public ushort m_ushort;
		public object m_object = new object();
		public TimeSpan m_timeSpan = TimeSpan.FromTicks(TimeSpan.TicksPerDay);
		public byte[] m_bytes = new byte[10];
		public DateTime m_time = DateTime.Now;
	}

	[Serializable]
	class SerializableClass : ISerializable
	{
		private DateTime m_time = DateTime.Parse("2022-05-07T10:40:46.0618350-04:00");
		public DateTime Time => m_time;

		public SerializableClass() { }
		public SerializableClass(SerializationInfo info, StreamingContext context)
		{
			m_time = info.GetDateTime(nameof(m_time));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(m_time), m_time);
		}
	}
	[Serializable]
	class Serializable2Class : ISerializable
	{
		private TimeSpan m_time = new TimeSpan(TimeSpan.TicksPerDay);
		public TimeSpan Time => m_time;

		public Serializable2Class() { }
		public Serializable2Class(SerializationInfo info, StreamingContext context)
		{
			m_time = (TimeSpan)info.GetValue(nameof(m_time), typeof(TimeSpan));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(nameof(m_time), m_time);
		}
	}
}
