/// <summary>
/// ģ�����������
/// </summary>
public abstract class ManagerBase
{
    /// <summary>
    /// ģ�����ȼ������ȼ��ߵ�ģ����ȱ���ѯ�����Һ�ر�
    /// </summary>
    public virtual int Priority
    {
        get
        {
            return 0;
        }
    }

    /// <summary>
    /// ��ʼ��ģ��
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// ��ѯģ��
    /// </summary>
    /// <param name="elapseSeconds">�߼�������</param>
    /// <param name="realElapseSeconds">��ʵ������</param>
    public abstract void Update(float elapseSeconds, float realElapseSeconds);

    /// <summary>
    /// ֹͣ������ģ��
    /// </summary>
    public abstract void Shutdown();
}