import { Form, Input, Button, Checkbox } from 'antd';

const Index = () => {
  const onFinish = (values) => {
    console.log('Success:', values);
  };

  const onFinishFailed = (errorInfo) => {
    console.log('Failed:', errorInfo);
  };
  return (
    <div
      className='login-form'
      style={{
        display: 'flex',
        alignItems: 'center',
        flexDirection: 'column',
      }}
    >
      <div>
        <Form
          name='basic'
          initialValues={{ remember: true }}
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          autoComplete='off'
        >
          <Form.Item
            label='Account Number'
            name='acnumber'
            rules={[
              {
                required: true,
                message: 'Please input your 11-digit account number!',
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label='6-Digit Pincode'
            name='pincode'
            rules={[
              { required: true, message: 'Please input your 6-digit pincode!' },
            ]}
          >
            <Input.Password />
          </Form.Item>
          <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
            <Button type='primary' htmlType='submit'>
              Submit
            </Button>
          </Form.Item>
        </Form>
      </div>
    </div>
  );
};

export default Index;
