using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TestSearch
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
            iconList.Images.Add("folder", new Bitmap(Properties.Resources.folder_open.ToBitmap()));
            iconList.Images.Add("document", new Bitmap(Properties.Resources.Generic_Document.ToBitmap()));
            dirTextBox.Text=Properties.Settings.Default.targetDirectory;
        /*    addToTree(@"H:\Downloads\Antimatter\");
            addToTree(@"H:\Downloads\Arven - Music Of Light - 2011 (320 kbps)");
            addToTree(@"H:\Downloads\Dominia - Judgement Of Tormented Souls\01. Prelude.mp3");*/
            
        }

        private void dirTextBox_Leave(object sender, EventArgs e)
        {
            Properties.Settings.Default.targetDirectory = dirTextBox.Text;
            Properties.Settings.Default.Save();
        }

        private void dirTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Properties.Settings.Default.targetDirectory = dirTextBox.Text;
                Properties.Settings.Default.Save();
            }
        }

        private bool addToTree(string fullPath)
        {
            FileAttributes attr; //Let's validate our path
            try
            {
                 attr = File.GetAttributes(fullPath);
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine("File not found: " + fullPath);
                return false;
            }
            catch(DirectoryNotFoundException ex)
            {
                Console.WriteLine("Directory not found: " + fullPath);
                return false;
            }
            catch
            {
                Console.WriteLine("Internal error: " + fullPath);
                return false;
            }

            string[] dirs = fullPath.Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            string rootPath=Properties.Settings.Default.targetDirectory;
            string path=rootPath;
            string[] rootDirs =rootPath.Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            int index = 0;
            bool found = false;
            bool isDirectory = ((attr & FileAttributes.Directory) == FileAttributes.Directory);
           
            while (index<rootDirs.Length && dirs[index] == rootDirs[index] ) index++;
            index--; //Now it's root directory on both
            if (index < 0) return false; //Not a subdirectory of root
            //let's find root
            TreeNode currentNode = new TreeNode();
            foreach (TreeNode node in resultView.Nodes)
            {
                if(node.Text==dirs[index])
                {
                    found = true;
                    currentNode = node;
                    break;
                }
            }
            if(!found)//No root, eh? Let's create it!
            {
                currentNode = new TreeNode(dirs[index]);
                currentNode.Tag = new DirectoryInfo(rootPath);
                currentNode.ImageKey = "folder";
                resultView.Nodes.Add(currentNode);
                currentNode.Expand();
                
            }
            index++;
            //now let's find, where it branches
            for(; index<dirs.Length; index++)
            {
                found = false;
                foreach (TreeNode node in currentNode.Nodes)
                {
                    if(node.Text==dirs[index])
                    {
                        found = true;
                        currentNode = node;
                        path+=Path.DirectorySeparatorChar+node.Text;
                        break;
                    }
                    
                }
                if (!found) break; //Last found node is "current node" now
                //found our branch;
            }
            //adding branch
            //folders
            int shift=0;
            if (!isDirectory) shift = 1;
            TreeNode addNode;
            for (; index < dirs.Length-shift; index++ )
            {
                path+=Path.DirectorySeparatorChar+dirs[index];
                addNode=new TreeNode(dirs[index]);
                addNode.Tag = new DirectoryInfo(path);
                addNode.ImageKey = "folder";
                currentNode.Nodes.Add(addNode);
                currentNode.Expand();
                currentNode = addNode;
            }
            //files?
            if (!isDirectory)
            {
                path += Path.DirectorySeparatorChar + dirs[index];
                addNode = new TreeNode(dirs[index]);
                addNode.Tag = new FileInfo(path);
                addNode.ImageKey = "document";
                currentNode.Nodes.Add(addNode);
                currentNode.Expand();
            }
            return true;
            
        }

        private void treeSearch(DirectoryInfo directory, string pattern)
        {
            DirectoryInfo[] subDirs={};
            try
            {
                subDirs = directory.GetDirectories();
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Directory not found: " + directory.Name);
            }
            catch (System.Security.SecurityException ex)
            {
                Console.WriteLine("Security exception: " + directory.Name);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("No rights to access: " + directory.Name);
            }
            catch
            {
                Console.WriteLine(directory.FullName);
            }
           /* foreach (DirectoryInfo dir in subDirs)
            {
                FileInfo[] sub_matches=dir.GetFiles(pattern);
                foreach(FileInfo match in matches)
                {
                    addToTree(match.FullName);
                }
                treeSearch(dir, pattern);
            }*/
           
            foreach (DirectoryInfo dir in subDirs)
            {
                treeSearch(dir, pattern);
            }
            FileInfo[] matches = directory.GetFiles(pattern);
            foreach (FileInfo match in matches)
            {
                addToTree(match.FullName);
            }
           // treeSearch(dir, pattern);
            
            
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            //start with validating path
            string folderPath = Properties.Settings.Default.targetDirectory;
            resultView.Nodes.Clear();
            FileAttributes attr; //Let's validate our path
            try
            {
                attr = File.GetAttributes(folderPath);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File not found: " + folderPath);
                return;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Directory not found: " + folderPath);
                return;
            }
            catch
            {
                Console.WriteLine("Internal error: " + folderPath);
                return;
            }
            bool isDirectory = ((attr & FileAttributes.Directory) == FileAttributes.Directory);
            folderPath = folderPath.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            if (!isDirectory) folderPath = folderPath.Remove(folderPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            treeSearch(new DirectoryInfo(folderPath), templateTextBox.Text);
        //    string[] dirs = folder.Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });


        }

      
    }
}
