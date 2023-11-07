using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

public abstract class NetworkWorkerThread
{
	private Thread thread = null;
	protected bool shutdown = false;

	public void Start()
	{
		thread = new Thread (ReceiveData);
		thread.IsBackground = true;
		thread.Start();
	}

	public void Shutdown()
	{
		shutdown = true;
		thread.Join();
	}

	protected abstract void ReceiveData();
	protected abstract bool SendData();
}
