using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FamilyTreeInfoPanel : MonoBehaviour
{
    class TreeNode<T>
    {
        public int Id
        {
            get;
            private set;
        }
        public T Obj
        {
            get;
            private set;
        }
        public List<TreeNode<T>> m_childs = new List<TreeNode<T>>();

        public TreeNode(int id, T obj)
        {
            Id = id;
            Obj = obj;
        }
    }
    [SerializeField]
    GameObject m_nodeUIPref = null;
    Dictionary<int, TreeNode<HUDFamilyTreeNode>> m_trees = new Dictionary<int, TreeNode<HUDFamilyTreeNode>>();

    void Awake()
    {
        TreeNode<HUDFamilyTreeNode> treeNode = CreateTreeNode();
        treeNode.Obj.SiblingCount = 1;
        treeNode.Obj.SiblingOrder = 0;

        AddChilds(treeNode.Id, 3);
        AddChilds(1, 2);
        AddChilds(2, 3);
        AddChilds(3, 2);

        AddChilds(7, 7);
        AddChilds(13, 2);

        AdjPos(0);

        Debug.Log("0:" + GetMaxChildCount(0));
        Debug.Log("1:" + GetMaxChildCount(1));
        Debug.Log("2:" + GetMaxChildCount(2));
        Debug.Log("3:" + GetMaxChildCount(3));
    }

    TreeNode<HUDFamilyTreeNode> CreateTreeNode()
    {
        int nodeID = m_trees.Count;
        GameObject obj = Instantiate(m_nodeUIPref) as GameObject;
        HUDFamilyTreeNode node = obj.GetComponent<HUDFamilyTreeNode>();
        TreeNode<HUDFamilyTreeNode> treeNode = new TreeNode<HUDFamilyTreeNode>(nodeID, node);
        treeNode.Obj.transform.SetParent(transform);
        treeNode.Obj.transform.localScale = m_nodeUIPref.transform.localScale;
        treeNode.Obj.GetComponentInChildren<Text>().text = nodeID.ToString();
        m_trees.Add(nodeID, treeNode);

        return treeNode;
    }

    void AddChilds(int nodeID, int childCount)
    {
        if (false == m_trees.ContainsKey(nodeID))
            return;

        for (int i = 0; i < childCount; ++i)
        {
            TreeNode<HUDFamilyTreeNode> treeNode = CreateTreeNode();
            treeNode.Obj.transform.SetParent(m_trees[nodeID].Obj.transform);
            treeNode.Obj.transform.localScale = m_nodeUIPref.transform.localScale;
            treeNode.Obj.SiblingCount = childCount;
            treeNode.Obj.SiblingOrder = i;

            m_trees[nodeID].m_childs.Add(treeNode);
        }
    }

    int GetMaxChildCount(int nodeID)
    {
        if (false == m_trees.ContainsKey(nodeID))
            return 0;

        int count = m_trees[nodeID].m_childs.Count;
        foreach (var node in m_trees[nodeID].m_childs)
        {
            int c = GetMaxChildCount(node.Id);
            if (count < c)
                count = c;
        }

        return count;
    }

    int AdjPos(int nodeID)
    {
        if (false == m_trees.ContainsKey(nodeID))
            return 0;

        if (0 == m_trees[nodeID].m_childs.Count)
            return 0;

        int[] childCount = new int[m_trees[nodeID].m_childs.Count];
        int totalChildCount = 0;
        for (int i = 0; i < m_trees[nodeID].m_childs.Count; ++i)
        {
            var node = m_trees[nodeID].m_childs[i];
            childCount[i] += AdjPos(node.Id);
            childCount[i] += node.m_childs.Count + node.m_childs.Count / 2 * 2;
            totalChildCount += childCount[i];
        }

        for (int i = 0; i < m_trees[nodeID].m_childs.Count; ++i)
        {
            var node = m_trees[nodeID].m_childs[i];
            node.Obj.PaddingWidth = 100 * (totalChildCount);
            node.Obj.paddingCount = totalChildCount;
        }

        return totalChildCount;
    }

}