﻿//============================================================================*
// cLoad.cs
//
// Copyright © 2013-2014, Kevin S. Beebe
// All Rights Reserved
//============================================================================*

//============================================================================*
// .Net Using Statements
//============================================================================*

using System;
using System.Xml;

//============================================================================*
// NameSpace
//============================================================================*

namespace ReloadersWorkShop
	{
	//============================================================================*
	// cLoad class
	//============================================================================*

	[Serializable]
	public class cLoad : IComparable
		{
		//----------------------------------------------------------------------------*
		// Private Data Members
		//----------------------------------------------------------------------------*

		private cFirearm.eFireArmType m_eFirearmType = cFirearm.eFireArmType.Handgun;
		private cCaliber m_Caliber;
		private cBullet m_Bullet = null;
		private cPowder m_Powder = null;
		private cCase m_Case = null;
		private cPrimer m_Primer = null;

		private cChargeList m_ChargeList = new cChargeList();

		private bool m_fChecked = false;

		private bool m_fIdentity = false;

		//============================================================================*
		// cLoad() - Constructor
		//============================================================================*

		public cLoad(bool fIdentity = false)
			{
			m_fIdentity = fIdentity;
			}

		//============================================================================*
		// cLoad() - Copy Constructor
		//============================================================================*

		public cLoad(cLoad Load)
			{
			m_eFirearmType = Load.m_eFirearmType;
			m_Caliber = Load.m_Caliber;
			m_Bullet = Load.m_Bullet;
			m_Powder = Load.m_Powder;
			m_Case = Load.m_Case;
			m_Primer = Load.m_Primer;
			m_fChecked = Load.m_fChecked;
			m_fIdentity = Load.m_fIdentity;

			m_ChargeList = new cChargeList(Load.m_ChargeList);
			}

		//============================================================================*
		// AddCharge()
		//============================================================================*

		public void AddCharge(cCharge NewCharge)
			{
			cCharge Charge = m_ChargeList.AddCharge(NewCharge);

			if (Charge.CompareTo(NewCharge) != 0)
				{
				foreach (cChargeTest ChargeTest in NewCharge.TestList)
					Charge.AddTest(ChargeTest);
				}
			}

		//============================================================================*
		// Bullet Property
		//============================================================================*

		public cBullet Bullet
			{
			get
				{
				return (m_Bullet);
				}
			set
				{
				m_Bullet = value;
				}
			}

		//============================================================================*
		// Caliber Property
		//============================================================================*

		public cCaliber Caliber
			{
			get
				{
				return (m_Caliber);
				}
			set
				{
				m_Caliber = value;
				}
			}

		//============================================================================*
		// Case Property
		//============================================================================*

		public cCase Case
			{
			get
				{
				return (m_Case);
				}
			set
				{
				m_Case = value;
				}
			}

		//============================================================================*
		// ChargeList Property
		//============================================================================*

		public cChargeList ChargeList
			{
			get
				{
				return (m_ChargeList);
				}
			set
				{
				m_ChargeList = value;
				}
			}

		//============================================================================*
		// Checked Property
		//============================================================================*

		public bool Checked
			{
			get
				{
				return (m_fChecked);
				}
			set
				{
				m_fChecked = value;
				}
			}

		//============================================================================*
		// Comparer()
		//============================================================================*

		public static int Comparer(cLoad Load1, cLoad Load2)
			{
			if (Load1 == null)
				{
				if (Load2 != null)
					return (-1);
				else
					return (0);
				}
			else
				{
				if (Load2 == null)
					return (1);
				}

			return (Load1.CompareTo(Load2));
			}

		//============================================================================*
		// CompareTo()
		//============================================================================*

		public int CompareTo(Object obj)
			{
			if (obj == null)
				return (1);

			cLoad Load = (cLoad) obj;

			//----------------------------------------------------------------------------*
			// FirearmType
			//----------------------------------------------------------------------------*

			int rc = m_eFirearmType.CompareTo(Load.m_eFirearmType);

			//----------------------------------------------------------------------------*
			// Caliber
			//----------------------------------------------------------------------------*

			if (rc == 0)
				{
				if (m_Caliber == null)
					rc = -1;
				else
					rc = m_Caliber.CompareTo(Load.m_Caliber);

				//----------------------------------------------------------------------------*
				// Bullet
				//----------------------------------------------------------------------------*

				if (rc == 0)
					{
					if (m_Bullet == null)
						rc = -1;
					else
						rc = m_Bullet.CompareTo(Load.m_Bullet);

					//----------------------------------------------------------------------------*
					// Powder
					//----------------------------------------------------------------------------*

					if (rc == 0)
						{
						if (m_Powder == null)
							rc = -1;
						else
							rc = m_Powder.CompareTo(Load.m_Powder);

						//----------------------------------------------------------------------------*
						// Case
						//----------------------------------------------------------------------------*

						if (rc == 0)
							{
							if (m_Case == null)
								rc = -1;
							else
								rc = m_Case.CompareTo(Load.m_Case);

							//----------------------------------------------------------------------------*
							// Primer
							//----------------------------------------------------------------------------*

							if (rc == 0)
								{
								if (m_Primer == null)
									rc = -1;
								else
									rc = m_Primer.CompareTo(Load.m_Primer);
								}
							}
						}
					}
				}

			return (rc);
			}

		//============================================================================*
		// CSVHeader Property
		//============================================================================*

		public static string CSVHeader
			{
			get
				{
				return ("Loads");
				}
			}

		//============================================================================*
		// CSVLine Property
		//============================================================================*

		public string CSVLine
			{
			get
				{
				string strLine = "";

				strLine += cFirearm.FirearmTypeString(FirearmType);
				strLine += ",";
				strLine += m_Caliber;
				strLine += ",";

				strLine += m_Bullet.ToString();
				strLine += ",";

				strLine += m_Powder.ToString();
				strLine += ",";

				strLine += m_Case.ToString();
				strLine += ",";

				strLine += m_Primer.ToString();
				strLine += ",";

				return (strLine);
				}
			}

		//============================================================================*
		// CSVLineHeader Property
		//============================================================================*

		public static string CSVLineHeader
			{
			get
				{
				return ("Firearm Type,Caliber,Bullet,Powder,Case,Primer");
				}
			}

		//============================================================================*
		// Export() - XML Document
		//============================================================================*

		public void Export(XmlDocument XMLDocument, XmlElement XMLParentElement)
			{
			XmlElement XMLThisElement = XMLDocument.CreateElement("Load");
			XMLParentElement.AppendChild(XMLThisElement);

			cCaliber.CurrentFirearmType = FirearmType;

			// Caliber

			m_Caliber.ExportIdentity(XMLDocument, XMLThisElement);

			// Firearm Type

			XmlElement XMLElement = XMLDocument.CreateElement("FirearmType");
			XmlText XMLTextElement = XMLDocument.CreateTextNode(cFirearm.FirearmTypeString(FirearmType));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Bullet

			m_Bullet.ExportIdentity(XMLDocument, XMLThisElement);

			// Powder

			m_Powder.ExportIdentity(XMLDocument, XMLThisElement);

			// Primer

			m_Primer.ExportIdentity(XMLDocument, XMLThisElement);

			// Case

			m_Case.ExportIdentity(XMLDocument, XMLThisElement);

			if (m_ChargeList.Count > 0)
				m_ChargeList.Export(XMLDocument, XMLThisElement);
			}

		//============================================================================*
		// ExportIdentity()
		//============================================================================*

		public void ExportIdentity(XmlDocument XMLDocument, XmlElement XMLParentElement)
			{
			XmlElement XMLThisElement = XMLDocument.CreateElement("FirearmIdentity");
			XMLParentElement.AppendChild(XMLThisElement);

			// Firearm Type

			XmlElement XMLElement = XMLDocument.CreateElement("FirearmType");
			XmlText XMLTextElement = XMLDocument.CreateTextNode(cFirearm.FirearmTypeString(m_eFirearmType));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Caliber

			m_Caliber.ExportIdentity(XMLDocument, XMLThisElement);

			// Bullet

			m_Bullet.ExportIdentity(XMLDocument, XMLThisElement);

			// Case

			m_Case.ExportIdentity(XMLDocument, XMLThisElement);

			// Powder

			m_Powder.ExportIdentity(XMLDocument, XMLThisElement);

			// Primer

			m_Primer.ExportIdentity(XMLDocument, XMLThisElement);
			}

		//============================================================================*
		// FirearmType Property
		//============================================================================*

		public cFirearm.eFireArmType FirearmType
			{
			get
				{
				return (m_eFirearmType);
				}
			set
				{
				m_eFirearmType = value;
				}
			}

		//============================================================================*
		// Identity Property
		//============================================================================*

		public bool Identity
			{
			get
				{
				return (m_fIdentity);
				}
			}

		//============================================================================*
		// Import()
		//============================================================================*

		public bool Import(XmlDocument XMLDocument, XmlNode XMLThisNode, cDataFiles DataFiles)
			{
			XmlNode XMLNode = XMLThisNode.FirstChild;

			while (XMLNode != null)
				{
				switch (XMLNode.Name)
					{
					case "BulletIdentity":
						m_Bullet = cDataFiles.GetBulletByIdentity(XMLDocument, XMLNode, DataFiles);
						break;
					case "PowderIdentity":
						m_Powder = cDataFiles.GetPowderByIdentity(XMLDocument, XMLNode, DataFiles);
						break;
					case "CaliberIdentity":
						m_Caliber = cDataFiles.GetCaliberByIdentity(XMLDocument, XMLNode, DataFiles);
						break;
					case "FirearmType":
						m_eFirearmType = cFirearm.FirearmTypeFromString(XMLNode.FirstChild.Value);
						break;
					case "PrimerIdentity":
						m_Primer = cDataFiles.GetPrimerByIdentity(XMLDocument, XMLNode, DataFiles);
						break;
					case "CaseIdentity":
						m_Case = cDataFiles.GetCaseByIdentity(XMLDocument, XMLNode, DataFiles);
						break;
					case "Charges":
					case "ChargeList":
						m_ChargeList.Import(XMLDocument, XMLNode, DataFiles);
						break;
					default:
						break;
					}

				XMLNode = XMLNode.NextSibling;
				}

			return (Validate());
			}

		//============================================================================*
		// Powder Property
		//============================================================================*

		public cPowder Powder
			{
			get
				{
				return (m_Powder);
				}
			set
				{
				m_Powder = value;
				}
			}

		//============================================================================*
		// Primer Property
		//============================================================================*

		public cPrimer Primer
			{
			get
				{
				return (m_Primer);
				}
			set
				{
				m_Primer = value;
				}
			}

		//============================================================================*
		// ResolveIdentities()
		//============================================================================*

		public bool ResolveIdentities(cDataFiles DataFiles)
			{
			bool fChanged = false;

			//----------------------------------------------------------------------------*
			// Caliber
			//----------------------------------------------------------------------------*

			if (m_Caliber.Identity)
				{
				foreach (cCaliber Caliber in DataFiles.CaliberList)
					{
					if (!Caliber.Identity && Caliber.CompareTo(m_Caliber) == 0)
						{
						m_Caliber = Caliber;

						fChanged = true;

						break;
						}
					}
				}

			//----------------------------------------------------------------------------*
			// Bullet
			//----------------------------------------------------------------------------*

			if (m_Bullet.Identity)
				{
				foreach (cBullet Bullet in DataFiles.BulletList)
					{
					if (!Bullet.Identity && m_Bullet.CompareTo(Bullet) == 0)
						{
						m_Bullet = Bullet;

						fChanged = true;

						break;
						}
					}
				}

			//----------------------------------------------------------------------------*
			// Case
			//----------------------------------------------------------------------------*

			if (m_Case.Identity)
				{
				foreach (cCase Case in DataFiles.CaseList)
					{
					if (!Case.Identity && m_Case.CompareTo(Case) == 0)
						{
						m_Case = Case;

						fChanged = true;

						break;
						}
					}
				}

			//----------------------------------------------------------------------------*
			// Powder
			//----------------------------------------------------------------------------*

			if (m_Powder.Identity)
				{
				foreach (cPowder Powder in DataFiles.PowderList)
					{
					if (!Powder.Identity && m_Powder.CompareTo(Powder) == 0)
						{
						m_Powder = Powder;

						fChanged = true;

						break;
						}
					}
				}

			//----------------------------------------------------------------------------*
			// Primer
			//----------------------------------------------------------------------------*

			if (m_Primer.Identity)
				{
				foreach (cPrimer Primer in DataFiles.PrimerList)
					{
					if (!Primer.Identity && m_Primer.CompareTo(Primer) == 0)
						{
						m_Primer = Primer;

						fChanged = true;

						break;
						}
					}
				}

			//----------------------------------------------------------------------------*
			// Charge List
			//----------------------------------------------------------------------------*

			fChanged = m_ChargeList.ResolveIdentities(DataFiles) ? true : fChanged;

			return (fChanged);
			}

		//============================================================================*
		// Synch() - Bullet
		//============================================================================*

		public bool Synch(cBullet Bullet)
			{
			if (m_Bullet != null && m_Bullet.CompareTo(Bullet) == 0)
				{
				m_Bullet = Bullet;

				return (true);
				}

			return (false);
			}

		//============================================================================*
		// Synch() - Caliber
		//============================================================================*

		public bool Synch(cCaliber Caliber)
			{
			if (m_Caliber != null && m_Caliber.CompareTo(Caliber) == 0)
				{
				m_Caliber = Caliber;

				return (true);
				}

			return (false);
			}

		//============================================================================*
		// Synch() - Case
		//============================================================================*

		public bool Synch(cCase Case)
			{
			if (m_Case != null && m_Case.CompareTo(Case) == 0)
				{
				m_Case = Case;

				return (true);
				}

			return (false);
			}

		//============================================================================*
		// Synch() - Firearm
		//============================================================================*

		public bool Synch(cFirearm Firearm)
			{
			bool fFound = false;

			foreach (cCharge CheckCharge in m_ChargeList)
				fFound = CheckCharge.Synch(Firearm);

			return (fFound);
			}

		//============================================================================*
		// Synch() - Powder
		//============================================================================*

		public bool Synch(cPowder Powder)
			{
			if (m_Powder != null && m_Powder.CompareTo(Powder) == 0)
				{
				m_Powder = Powder;

				return (true);
				}

			return (false);
			}

		//============================================================================*
		// Synch() - Primer
		//============================================================================*

		public bool Synch(cPrimer Primer)
			{
			if (m_Primer != null && m_Primer.CompareTo(Primer) == 0)
				{
				m_Primer = Primer;

				return (true);
				}

			return (false);
			}

		//============================================================================*
		// ToShortString Property
		//============================================================================*

		public string ToShortString()
			{
			string strLoadString = m_Bullet.ToShortString();
			strLoadString += " Bullet, ";
			strLoadString += m_Powder.ToString();
			strLoadString += " Powder";

			return (strLoadString);
			}

		//============================================================================*
		// ToString Property
		//============================================================================*

		public override string ToString()
			{
			string strLoadString = m_Bullet.ToShortString();
			strLoadString += " Bullet, ";
			strLoadString += m_Powder.ToString();
			strLoadString += " Powder, ";
			strLoadString += Case.ToShortString();
			strLoadString += " Case, ";
			strLoadString += Primer.ToShortString();
			strLoadString += " Primer";

			return (strLoadString);
			}

		//============================================================================*
		// Validate()
		//============================================================================*

		public bool Validate()
			{
			bool fOK = m_Caliber != null &&
						m_Bullet != null &&
						m_Powder != null &&
						m_Case != null &&
						m_Primer != null;

			if (Identity)
				return (fOK);

			if (fOK)
				fOK = m_ChargeList.Count > 0;

			return (fOK);
			}
		}
	}
