namespace Pretty.Core.Domain.Roles
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }

        public int RoleLevel { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string RoleRemark { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUserid { get; set; }

    }
}
