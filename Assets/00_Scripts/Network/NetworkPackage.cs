﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net.Sockets;

public class NetworkPackage
{
	private int maxSize = 2048;
	private List<NetworkPackageValue> valueList = new List<NetworkPackageValue>();

	//Public Values

	public void Clear() {valueList.Clear();}

	//Returns the count of NetworkPackageValues
	public int Count { get {return valueList.Count;} }

	//Sets the value of an existing NetworkPackageValue
	public void AddValue(NetworkPackageValue val)
	{
		valueList.Add(val);
	}

	//Returns the value for the given index
	public NetworkPackageValue Value(int index)
	{
		return valueList[index];
	}

	//Returns the sum of the size of the contents in bytes
	public int Size()
	{
		int size = 0;

		foreach (var i in valueList)
			size += i.Size();

		return size;
	}

	//Sends the serilized content via the socket 
	public void Send(Socket socket)
	{
		socket.Send(GetSerializedData());
	}

	//Try to read package data from socket
	public bool Receive (Socket socket)
	{
		if (socket != null)
		{
			byte[] buffer = new byte[maxSize];
			socket.Receive(buffer);
			return ReadBytes(buffer);
		}

		return false;
	}

	public byte[] GetSerializedData()
	{
		//Init byte array with package size
		byte[] value = new byte[Size()];

		int index = 0;

		foreach (var i in valueList)
		{
			int size = i.Size();

			//copy content bytes to the correct position in value byte array
			Buffer.BlockCopy(i.GetSerializedData(), 0, value, index, size);

			//Adding current size to index offset to get the memmory position for the next entry 
			index += size;
		}

		return value;
	}

	//Private Functions
	public bool ReadBytes(byte[] bytes)
	{
		int index = 0;
		valueList.Clear();

		try
		{
			while (index < bytes.Length)
			{
				//Read value size from memmory
				int size = BitConverter.ToInt32(bytes, index);

				if (size == 0)
					break;
				else
					size += NetworkPackageValue.StartIndex;

				//Read value from memmory
				byte[] buffer = new byte[size];
				Buffer.BlockCopy(bytes, index, buffer, 0, size);
				NetworkPackageValue tmp = new NetworkPackageValue();
				tmp.DeserializeData (buffer);
				valueList.Add (tmp);

				//Adding current size to index offset to get the memmory position for the next entry 
				index += size;
			}

			return true;
		}
		catch (Exception excp)
		{
			return false;
		}
	}
}

