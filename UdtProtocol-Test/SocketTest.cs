﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

using NUnit.Framework;
using System.Net;

namespace UdtProtocol_Test
{
    /// <summary>
    /// Test fixture for <see cref="Udt.Socket"/>.
    /// </summary>
    [TestFixture]
    public class SocketTest
    {
        /// <summary>
        /// Test for <see cref="Udt.Socket(AddressFamily,SocketType)"/> constructor.
        /// </summary>
        [Test]
        public void Constructor()
        {
            Udt.Socket socket = new Udt.Socket(AddressFamily.InterNetwork, SocketType.Stream);
            Assert.AreEqual(AddressFamily.InterNetwork, socket.AddressFamily);
            Assert.IsTrue(socket.BlockingReceive);
            Assert.IsTrue(socket.BlockingSend);
            Assert.IsNull(socket.CongestionControl);
            LingerOption opt = socket.LingerState;
            Assert.IsTrue(opt.Enabled);
            Assert.AreEqual(180, opt.LingerTime);
            Assert.Throws<Udt.SocketException>(() => { IPEndPoint ep = socket.LocalEndPoint; });
            Assert.AreEqual(-1, socket.MaxBandwidth);
            Assert.AreEqual(1052, socket.MaxPacketSize);
            Assert.AreEqual(25600, socket.MaxWindowSize);
            Assert.AreEqual(8388608, socket.ReceiveBufferSize);
            Assert.AreEqual(-1, socket.ReceiveTimeout);
            Assert.Throws<Udt.SocketException>(() => { IPEndPoint ep = socket.RemoteEndPoint; });
            Assert.IsFalse(socket.Rendezvous);
            Assert.IsTrue(socket.ReuseAddress);
            Assert.AreEqual(8388608, socket.SendBufferSize);
            Assert.AreEqual(-1, socket.SendTimeout);
            Assert.AreEqual(SocketType.Stream, socket.SocketType);
            Assert.AreEqual(12288000, socket.UdpReceiveBufferSize);
            Assert.AreEqual(65536, socket.UdpSendBufferSize);
        }

        /// <summary>
        /// Test for <see cref="Udt.Socket(AddressFamily,SocketType)"/> constructor
        /// with all the valid argument combinations.
        /// </summary>
        [Test]
        public void Constructor__ValidArgs()
        {
            // InterNetwork
            Udt.Socket socket = new Udt.Socket(AddressFamily.InterNetwork, SocketType.Dgram);
            Assert.AreEqual(AddressFamily.InterNetwork, socket.AddressFamily);
            Assert.AreEqual(SocketType.Dgram, socket.SocketType);

            socket = new Udt.Socket(AddressFamily.InterNetwork, SocketType.Stream);
            Assert.AreEqual(AddressFamily.InterNetwork, socket.AddressFamily);
            Assert.AreEqual(SocketType.Stream, socket.SocketType);

            // InterNetworkV6
            socket = new Udt.Socket(AddressFamily.InterNetworkV6, SocketType.Dgram);
            Assert.AreEqual(AddressFamily.InterNetworkV6, socket.AddressFamily);
            Assert.AreEqual(SocketType.Dgram, socket.SocketType);

            socket = new Udt.Socket(AddressFamily.InterNetworkV6, SocketType.Stream);
            Assert.AreEqual(AddressFamily.InterNetworkV6, socket.AddressFamily);
            Assert.AreEqual(SocketType.Stream, socket.SocketType);
        }

        /// <summary>
        /// Test for <see cref="Udt.Socket(AddressFamily,SocketType)"/> constructor
        /// with invalid argument values.
        /// </summary>
        [Test]
        public void Constructor__InvalidArgs()
        {
            // family
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new Udt.Socket(AddressFamily.ImpLink, SocketType.Stream));
            Assert.AreEqual("family", ex.ParamName);

            ex = Assert.Throws<ArgumentException>(() => new Udt.Socket(AddressFamily.Ipx, SocketType.Stream));
            Assert.AreEqual("family", ex.ParamName);

            // type
            ex = Assert.Throws<ArgumentException>(() => new Udt.Socket(AddressFamily.InterNetwork, SocketType.Raw));
            Assert.AreEqual("type", ex.ParamName);

            ex = Assert.Throws<ArgumentException>(() => new Udt.Socket(AddressFamily.InterNetwork, SocketType.Rdm));
            Assert.AreEqual("type", ex.ParamName);

            ex = Assert.Throws<ArgumentException>(() => new Udt.Socket(AddressFamily.InterNetwork, SocketType.Seqpacket));
            Assert.AreEqual("type", ex.ParamName);

            ex = Assert.Throws<ArgumentException>(() => new Udt.Socket(AddressFamily.InterNetwork, SocketType.Unknown));
            Assert.AreEqual("type", ex.ParamName);
        }

        /// <summary>
        /// Test for <see cref="Udt.Socket.Bind(IPAddress,int)"/>.
        /// </summary>
        [Test]
        public void Bind_IPAddress_int()
        {
        }

        /// <summary>
        /// Test for <see cref="Udt.Socket.Bind(IPAddress,int)"/> with invalid
        /// argument values.
        /// </summary>
        [Test]
        public void Bind_IPAddress_int__InvalidArgs()
        {
            Udt.Socket socket = new Udt.Socket(AddressFamily.InterNetwork, SocketType.Stream);

            // address
            ArgumentException ex = Assert.Throws<ArgumentNullException>(() => socket.Bind(null, 1));
            Assert.AreEqual("address", ex.ParamName);

            // address type different from family passed to constructor
            ex = Assert.Throws<ArgumentException>(() => socket.Bind(IPAddress.IPv6Any, 10000));
            Assert.AreEqual("address", ex.ParamName);

            // port
            ArgumentOutOfRangeException argEx = Assert.Throws<ArgumentOutOfRangeException>(() => socket.Bind(IPAddress.Any, -1));
            Assert.AreEqual("port", argEx.ParamName);
            Assert.AreEqual(-1, argEx.ActualValue);

            argEx = Assert.Throws<ArgumentOutOfRangeException>(() => socket.Bind(IPAddress.Any, 65536));
            Assert.AreEqual("port", argEx.ParamName);
            Assert.AreEqual(65536, argEx.ActualValue);
        }

        /// <summary>
        /// Test for <see cref="Udt.Socket.Bind(IPEndPoint)"/>.
        /// </summary>
        [Test]
        public void Bind_IPEndPoint()
        {
        }

        /// <summary>
        /// Test for <see cref="Udt.Socket.Bind(IPEndPoint)"/> with invalid
        /// argument values.
        /// </summary>
        [Test]
        public void Bind_IPEndPoint__InvalidArgs()
        {
            Udt.Socket socket = new Udt.Socket(AddressFamily.InterNetwork, SocketType.Stream);

            // endPoint
            ArgumentException ex = Assert.Throws<ArgumentNullException>(() => socket.Bind(null));
            Assert.AreEqual("endPoint", ex.ParamName);

            // address type different from family passed to constructor
            ex = Assert.Throws<ArgumentException>(() => socket.Bind(new IPEndPoint(IPAddress.IPv6Any, 10000)));
            Assert.AreEqual("endPoint", ex.ParamName);
        }
    }
}