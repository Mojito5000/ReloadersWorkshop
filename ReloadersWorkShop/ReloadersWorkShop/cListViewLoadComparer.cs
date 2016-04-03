﻿//============================================================================*
// cListViewLoadComparer.cs
//
// Copyright © 2013-2014, Kevin S. Beebe
// All Rights Reserved
//============================================================================*

//============================================================================*
// .Net Using Statements
//============================================================================*

using System;
using System.Collections;
using System.Windows.Forms;

//============================================================================*
// Namespace
//============================================================================*

namespace ReloadersWorkShop
	{
	//============================================================================*
	// cListViewLoadComparer Class
	//============================================================================*

	class cListViewLoadComparer : cListViewComparer
		{
		//============================================================================*
		// Private Data Members
		//============================================================================*

		//============================================================================*
		// cListViewLoadComparer() - Constructor
		//============================================================================*

		public cListViewLoadComparer(int nSortColumn, SortOrder SortOrder)
			: base(nSortColumn, SortOrder)
			{
			}

		//============================================================================*
		// Compare()
		//============================================================================*

		public override int Compare(Object Object1, Object Object2)
			{
			if (Object1 == null)
				{
				if (Object2 == null)
					return (0);
				else
					return (-1);
				}
			else
				{
				if (Object2 == null)
					return (1);
				}

			cLoad Load1 = (cLoad)(Object1 as ListViewItem).Tag;
			cLoad Load2 = (cLoad)(Object2 as ListViewItem).Tag;

			if (Load1 == null)
				{
				if (Load2 == null)
					return (0);
				else
					return (0);
				}
			else
				{
				if (Load2 == null)
					return (1);
				}

			//----------------------------------------------------------------------------*
			// Do Special Compares
			//----------------------------------------------------------------------------*

			bool fSpecial = false;
			int rc = 0;

			switch (SortColumn)
				{
				//----------------------------------------------------------------------------*
				// Caliber
				//----------------------------------------------------------------------------*

				case 0:
					rc = Load1.Caliber.CompareTo(Load2.Caliber);

					//----------------------------------------------------------------------------*
					// Bullet
					//----------------------------------------------------------------------------*

					if (rc == 0)
						{
						rc = Load1.Bullet.CompareTo(Load2.Bullet);

						//----------------------------------------------------------------------------*
						// Powder
						//----------------------------------------------------------------------------*

						if (rc == 0)
							{
							rc = Load1.Powder.CompareTo(Load2.Powder);

							//----------------------------------------------------------------------------*
							// Primer
							//----------------------------------------------------------------------------*

							if (rc == 0)
								{
								rc = Load1.Primer.CompareTo(Load2.Primer);

								//----------------------------------------------------------------------------*
								// Case
								//----------------------------------------------------------------------------*

								if (rc == 0)
									{
									rc = Load1.Case.CompareTo(Load2.Case);
									}
								}

							}
						}

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Bullet
				//----------------------------------------------------------------------------*

				case 1:
					rc = Load1.Bullet.CompareTo(Load2.Bullet);

					//----------------------------------------------------------------------------*
					// Caliber
					//----------------------------------------------------------------------------*

					if (rc == 0)
						{
						rc = Load1.Caliber.CompareTo(Load2.Caliber);

						//----------------------------------------------------------------------------*
						// Powder
						//----------------------------------------------------------------------------*

						if (rc == 0)
							{
							rc = Load1.Powder.CompareTo(Load2.Powder);

							//----------------------------------------------------------------------------*
							// Primer
							//----------------------------------------------------------------------------*

							if (rc == 0)
								{
								rc = Load1.Primer.CompareTo(Load2.Primer);

								//----------------------------------------------------------------------------*
								// Case
								//----------------------------------------------------------------------------*

								if (rc == 0)
									{
									rc = Load1.Case.CompareTo(Load2.Case);
									}
								}

							}
						}

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Powder
				//----------------------------------------------------------------------------*

				case 2:
					rc = Load1.Powder.CompareTo(Load2.Powder);

					//----------------------------------------------------------------------------*
					// Caliber
					//----------------------------------------------------------------------------*

					if (rc == 0)
						{
						rc = Load1.Caliber.CompareTo(Load2.Caliber);

						//----------------------------------------------------------------------------*
						// Bullet
						//----------------------------------------------------------------------------*

						if (rc == 0)
							{
							rc = Load1.Bullet.CompareTo(Load2.Bullet);

							//----------------------------------------------------------------------------*
							// Primer
							//----------------------------------------------------------------------------*

							if (rc == 0)
								{
								rc = Load1.Primer.CompareTo(Load2.Primer);

								//----------------------------------------------------------------------------*
								// Case
								//----------------------------------------------------------------------------*

								if (rc == 0)
									{
									rc = Load1.Case.CompareTo(Load2.Case);
									}
								}
							}
						}

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Primer
				//----------------------------------------------------------------------------*

				case 3:
					rc = Load1.Primer.Manufacturer.CompareTo(Load2.Primer.Manufacturer);

					//----------------------------------------------------------------------------*
					// Caliber
					//----------------------------------------------------------------------------*

					if (rc == 0)
						{
						rc = Load1.Caliber.CompareTo(Load2.Caliber);

						//----------------------------------------------------------------------------*
						// Bullet
						//----------------------------------------------------------------------------*

						if (rc == 0)
							{
							rc = Load1.Bullet.CompareTo(Load2.Bullet);

							//----------------------------------------------------------------------------*
							// Powder
							//----------------------------------------------------------------------------*

							if (rc == 0)
								{
								rc = Load1.Powder.CompareTo(Load2.Powder);

								//----------------------------------------------------------------------------*
								// Primer
								//----------------------------------------------------------------------------*

								if (rc == 0)
									{
									rc = Load1.Primer.CompareTo(Load2.Primer);

									//----------------------------------------------------------------------------*
									// Case
									//----------------------------------------------------------------------------*

									if (rc == 0)
										{
										rc = Load1.Case.CompareTo(Load2.Case);
										}
									}
								}
							}
						}

					fSpecial = true;

					break;

				//----------------------------------------------------------------------------*
				// Case
				//----------------------------------------------------------------------------*

				case 4:
					//----------------------------------------------------------------------------*
					// Case
					//----------------------------------------------------------------------------*

					rc = Load1.Case.CompareTo(Load2.Case);

					//----------------------------------------------------------------------------*
					// Caliber
					//----------------------------------------------------------------------------*

					if (rc == 0)
						{
						rc = Load1.Case.Caliber.CompareTo(Load2.Case.Caliber);

						//----------------------------------------------------------------------------*
						// Bullet
						//----------------------------------------------------------------------------*

						if (rc == 0)
							{
							rc = Load1.Bullet.CompareTo(Load2.Bullet);

							//----------------------------------------------------------------------------*
							// Powder
							//----------------------------------------------------------------------------*

							if (rc == 0)
								{
								rc = Load1.Powder.CompareTo(Load2.Powder);

								//----------------------------------------------------------------------------*
								// Primer
								//----------------------------------------------------------------------------*

								if (rc == 0)
									{
									rc = Load1.Primer.CompareTo(Load2.Primer);

									}
								}
							}
						}

					fSpecial = true;

					break;
				}

			if (fSpecial)
				{
				if (SortingOrder == SortOrder.Descending)
					return (0 - rc);

				return (rc);
				}

			return (base.Compare(Object1, Object2));
			}
		}
	}